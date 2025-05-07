using Microsoft.AspNetCore.Mvc;
using TravelPlanner.Entities;
using TravelPlanner.Services;

namespace TravelPlanner.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DestinationController : ControllerBase
{
    private readonly IDestinationService _destinationService;

    public DestinationController(IDestinationService destinationService)
    {
        _destinationService = destinationService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Destination>>> GetAll()
    {
        var destinations = await _destinationService.GetAllAsync();
        return Ok(destinations);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Destination>> GetById(int id)
    {
        var destination = await _destinationService.GetByIdAsync(id);
        if (destination == null)
            return NotFound();
        return Ok(destination);
    }

    [HttpPost]
    public async Task<ActionResult<Destination>> Create(Destination destination)
    {
        var createdDestination = await _destinationService.CreateAsync(destination);
        return CreatedAtAction(nameof(GetById), new { id = createdDestination.Id }, createdDestination);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Destination>> Update(int id, Destination destination)
    {
        var updatedDestination = await _destinationService.UpdateAsync(id, destination);
        return Ok(updatedDestination);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _destinationService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("country/{country}")]
    public async Task<ActionResult<IEnumerable<Destination>>> GetByCountry(string country)
    {
        var destinations = await _destinationService.GetDestinationsByCountryAsync(country);
        return Ok(destinations);
    }

    [HttpGet("popular")]
    public async Task<ActionResult<IEnumerable<Destination>>> GetPopular([FromQuery] int count = 10)
    {
        var destinations = await _destinationService.GetPopularDestinationsAsync(count);
        return Ok(destinations);
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<Destination>>> Search([FromQuery] string term)
    {
        var destinations = await _destinationService.SearchDestinationsAsync(term);
        return Ok(destinations);
    }

    [HttpGet("countries")]
    public async Task<ActionResult<IEnumerable<string>>> GetAllCountries()
    {
        var countries = await _destinationService.GetAllCountriesAsync();
        return Ok(countries);
    }

    [HttpGet("active")]
    public async Task<ActionResult<IEnumerable<Destination>>> GetActive()
    {
        var destinations = await _destinationService.GetDestinationsWithActiveTripsAsync();
        return Ok(destinations);
    }

    [HttpGet("sorted")]
    public async Task<ActionResult<IEnumerable<Destination>>> GetSorted([FromQuery] bool ascending = true)
    {
        var destinations = await _destinationService.GetDestinationsSortedByTripCountAsync(ascending);
        return Ok(destinations);
    }

    [HttpGet("statistics")]
    public async Task<ActionResult<Dictionary<string, int>>> GetStatistics()
    {
        var statistics = await _destinationService.GetDestinationStatisticsAsync();
        return Ok(statistics);
    }
} 