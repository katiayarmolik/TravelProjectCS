using TravelPlanner.Entities;

namespace TravelPlanner.Services;

public interface ITripService : IService<Trip>
{
    Task<IEnumerable<Trip>> GetPopularTripsAsync(int count = 10);
    Task IncrementPopularityAsync(int id);
    Task<IEnumerable<Trip>> GetTripsByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<IEnumerable<Trip>> GetTripsByPriceRangeAsync(decimal minPrice, decimal maxPrice);
    Task<IEnumerable<Trip>> GetTripsByDestinationAsync(int destinationId);
    Task<IEnumerable<Trip>> GetTripsByDepartureLocationAsync(int departureLocationId);
    Task<IEnumerable<Trip>> GetTripsByUserAsync(int userId);
    Task<IEnumerable<Trip>> GetTripsSortedByPriceAsync(bool ascending = true);
    Task<IEnumerable<Trip>> GetTripsSortedByDateAsync(bool ascending = true);
} 