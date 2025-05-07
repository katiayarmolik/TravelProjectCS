using TravelPlanner.Entities;

namespace TravelPlanner.Services;

public interface IDestinationService : IService<Destination>
{
    Task<IEnumerable<Destination>> GetDestinationsByCountryAsync(string country);
    Task<IEnumerable<Destination>> GetPopularDestinationsAsync(int count = 10);
    Task<IEnumerable<Destination>> SearchDestinationsAsync(string searchTerm);
    Task<IEnumerable<string>> GetAllCountriesAsync();
    Task<IEnumerable<Destination>> GetDestinationsWithActiveTripsAsync();
    Task<IEnumerable<Destination>> GetDestinationsSortedByTripCountAsync(bool ascending = true);
    Task<Dictionary<string, int>> GetDestinationStatisticsAsync();
} 