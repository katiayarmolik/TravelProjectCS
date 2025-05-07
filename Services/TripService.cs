using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TravelPlanner.Data;
using TravelPlanner.Entities;

namespace TravelPlanner.Services;

public class TripService : BaseService<Trip>, ITripService
{
    public TripService(TravelPlannerDbContext context) : base(context)
    {
    }

    public override async Task<IEnumerable<Trip>> GetAllAsync()
    {
        return await _context.Trips
            .Include(t => t.Destination)
            .Include(t => t.DepartureLocation)
            .Include(t => t.User)
            .ToListAsync();
    }

    public override async Task<Trip?> GetByIdAsync(int id)
    {
        return await _context.Trips
            .Include(t => t.Destination)
            .Include(t => t.DepartureLocation)
            .Include(t => t.User)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public override async Task<Trip> CreateAsync(Trip trip)
    {
        _context.Trips.Add(trip);
        await _context.SaveChangesAsync();
        return trip;
    }

    public override async Task<Trip> UpdateAsync(int id, Trip trip)
    {
        var existingTrip = await _context.Trips.FindAsync(id);
        if (existingTrip == null)
            throw new KeyNotFoundException($"Trip with ID {id} not found");

        _context.Entry(existingTrip).CurrentValues.SetValues(trip);
        await _context.SaveChangesAsync();
        return existingTrip;
    }

    public override async Task DeleteAsync(int id)
    {
        var trip = await _context.Trips.FindAsync(id);
        if (trip == null)
            throw new KeyNotFoundException($"Trip with ID {id} not found");

        _context.Trips.Remove(trip);
        await _context.SaveChangesAsync();
    }

    public override async Task<IEnumerable<Trip>> FindAsync(Expression<Func<Trip, bool>> predicate)
    {
        return await _context.Trips
            .Include(t => t.Destination)
            .Include(t => t.DepartureLocation)
            .Include(t => t.User)
            .Where(predicate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Trip>> GetPopularTripsAsync(int count = 10)
    {
        return await _context.Trips
            .Include(t => t.Destination)
            .Include(t => t.DepartureLocation)
            .Include(t => t.User)
            .OrderByDescending(t => t.PopularityCount)
            .Take(count)
            .ToListAsync();
    }

    public async Task IncrementPopularityAsync(int id)
    {
        var trip = await _context.Trips.FindAsync(id);
        if (trip == null)
            throw new KeyNotFoundException($"Trip with ID {id} not found");

        trip.PopularityCount++;
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Trip>> GetTripsByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _context.Trips
            .Include(t => t.Destination)
            .Include(t => t.DepartureLocation)
            .Include(t => t.User)
            .Where(t => t.StartDate >= startDate && t.EndDate <= endDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Trip>> GetTripsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
    {
        return await _context.Trips
            .Include(t => t.Destination)
            .Include(t => t.DepartureLocation)
            .Include(t => t.User)
            .Where(t => t.Price >= minPrice && t.Price <= maxPrice)
            .ToListAsync();
    }

    public async Task<IEnumerable<Trip>> GetTripsByDestinationAsync(int destinationId)
    {
        return await _context.Trips
            .Include(t => t.Destination)
            .Include(t => t.DepartureLocation)
            .Include(t => t.User)
            .Where(t => t.DestinationId == destinationId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Trip>> GetTripsByDepartureLocationAsync(int departureLocationId)
    {
        return await _context.Trips
            .Include(t => t.Destination)
            .Include(t => t.DepartureLocation)
            .Include(t => t.User)
            .Where(t => t.DepartureLocationId == departureLocationId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Trip>> GetTripsByUserAsync(int userId)
    {
        return await _context.Trips
            .Include(t => t.Destination)
            .Include(t => t.DepartureLocation)
            .Include(t => t.User)
            .Where(t => t.UserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Trip>> GetTripsSortedByPriceAsync(bool ascending = true)
    {
        var query = _context.Trips
            .Include(t => t.Destination)
            .Include(t => t.DepartureLocation)
            .Include(t => t.User);

        return ascending
            ? await query.OrderBy(t => t.Price).ToListAsync()
            : await query.OrderByDescending(t => t.Price).ToListAsync();
    }

    public async Task<IEnumerable<Trip>> GetTripsSortedByDateAsync(bool ascending = true)
    {
        var query = _context.Trips
            .Include(t => t.Destination)
            .Include(t => t.DepartureLocation)
            .Include(t => t.User);

        return ascending
            ? await query.OrderBy(t => t.StartDate).ToListAsync()
            : await query.OrderByDescending(t => t.StartDate).ToListAsync();
    }
} 