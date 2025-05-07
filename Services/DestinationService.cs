using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TravelPlanner.Data;
using TravelPlanner.Entities;

namespace TravelPlanner.Services;

public class DestinationService : BaseService<Destination>, IDestinationService
{
    public DestinationService(TravelPlannerDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Destination>> GetDestinationsByCountryAsync(string country)
    {
        return await _context.Destinations
            .Include(d => d.Trips)
            .Where(d => d.Country == country)
            .ToListAsync();
    }

    public async Task<IEnumerable<Destination>> GetPopularDestinationsAsync(int count = 10)
    {
        return await _context.Destinations
            .Include(d => d.Trips)
            .OrderByDescending(d => d.Trips.Count)
            .Take(count)
            .ToListAsync();
    }

    public async Task<IEnumerable<Destination>> SearchDestinationsAsync(string searchTerm)
    {
        return await _context.Destinations
            .Include(d => d.Trips)
            .Where(d => d.Name.Contains(searchTerm) || d.Country.Contains(searchTerm) || d.Description.Contains(searchTerm))
            .ToListAsync();
    }

    public async Task<IEnumerable<string>> GetAllCountriesAsync()
    {
        return await _context.Destinations
            .Select(d => d.Country)
            .Distinct()
            .ToListAsync();
    }

    public async Task<IEnumerable<Destination>> GetDestinationsWithActiveTripsAsync()
    {
        var currentDate = DateTime.UtcNow;
        return await _context.Destinations
            .Include(d => d.Trips)
            .Where(d => d.Trips.Any(t => t.StartDate <= currentDate && t.EndDate >= currentDate))
            .ToListAsync();
    }

    public async Task<IEnumerable<Destination>> GetDestinationsSortedByTripCountAsync(bool ascending = true)
    {
        var query = _context.Destinations
            .Include(d => d.Trips)
            .Select(d => new { Destination = d, TripCount = d.Trips != null ? d.Trips.Count : 0 });

        if (ascending)
            query = query.OrderBy(x => x.TripCount);
        else
            query = query.OrderByDescending(x => x.TripCount);

        return await query.Select(x => x.Destination).ToListAsync();
    }

    public async Task<Dictionary<string, int>> GetDestinationStatisticsAsync()
    {
        var destinations = await _context.Destinations
            .Include(d => d.Trips)
            .ToListAsync();

        return destinations
            .GroupBy(d => d.Country)
            .ToDictionary(
                g => g.Key,
                g => g.Sum(d => d.Trips.Count)
            );
    }
} 