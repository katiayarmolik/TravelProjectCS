using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TravelPlanner.Data;
using TravelPlanner.Entities;

namespace TravelPlanner.Services;

public class UserService : BaseService<User>, IUserService
{
    public UserService(TravelPlannerDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .Include(u => u.Trips)
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _context.Users
            .Include(u => u.Trips)
            .FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<IEnumerable<Trip>> GetUserTripsAsync(int userId)
    {
        return await _context.Trips
            .Include(t => t.Destination)
            .Include(t => t.DepartureLocation)
            .Where(t => t.UserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<User>> GetActiveUsersAsync()
    {
        var currentDate = DateTime.UtcNow;
        return await _context.Users
            .Include(u => u.Trips)
            .Where(u => u.Trips.Any(t => t.StartDate <= currentDate && t.EndDate >= currentDate))
            .ToListAsync();
    }

    public async Task<IEnumerable<User>> GetUsersByTripCountAsync(int minTrips)
    {
        return await _context.Users
            .Include(u => u.Trips)
            .Where(u => u.Trips.Count >= minTrips)
            .ToListAsync();
    }

    public async Task<Dictionary<string, int>> GetUserStatisticsAsync()
    {
        var users = await _context.Users
            .Include(u => u.Trips)
            .ToListAsync();

        return new Dictionary<string, int>
        {
            { "TotalUsers", users.Count },
            { "ActiveUsers", users.Count(u => u.Trips.Any()) },
            { "TotalTrips", users.Sum(u => u.Trips.Count) }
        };
    }

    public async Task<bool> UpdateUserPasswordAsync(int userId, string newPassword)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
            return false;

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ValidateUserCredentialsAsync(string email, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null)
            return false;

        return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
    }

    public async Task<IEnumerable<Trip>> GetUserUpcomingTripsAsync(int userId)
    {
        var currentDate = DateTime.UtcNow;
        return await _context.Trips
            .Include(t => t.Destination)
            .Include(t => t.DepartureLocation)
            .Where(t => t.UserId == userId && t.StartDate > currentDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Trip>> GetUserPastTripsAsync(int userId)
    {
        var currentDate = DateTime.UtcNow;
        return await _context.Trips
            .Include(t => t.Destination)
            .Include(t => t.DepartureLocation)
            .Where(t => t.UserId == userId && t.EndDate < currentDate)
            .ToListAsync();
    }
} 