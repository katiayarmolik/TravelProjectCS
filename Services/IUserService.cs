using TravelPlanner.Entities;

namespace TravelPlanner.Services;

public interface IUserService : IService<User>
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByUsernameAsync(string username);
    Task<IEnumerable<Trip>> GetUserTripsAsync(int userId);
    Task<IEnumerable<User>> GetActiveUsersAsync();
    Task<IEnumerable<User>> GetUsersByTripCountAsync(int minTrips);
    Task<Dictionary<string, int>> GetUserStatisticsAsync();
    Task<bool> UpdateUserPasswordAsync(int userId, string newPassword);
    Task<bool> ValidateUserCredentialsAsync(string email, string password);
    Task<IEnumerable<Trip>> GetUserUpcomingTripsAsync(int userId);
    Task<IEnumerable<Trip>> GetUserPastTripsAsync(int userId);
} 