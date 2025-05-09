using Microsoft.AspNetCore.Mvc;
using TravelPlanner.DTOs;
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
    public async Task<ActionResult<IEnumerable<DestinationResponseDTO>>> GetAll()
    {
        var destinations = await _destinationService.GetAllAsync();
        var response = destinations.Select(d => new DestinationResponseDTO
        {
            Id = d.Id,
            Name = d.Name,
            Country = d.Country,
            Description = d.Description,
            TripCount = d.Trips?.Count ?? 0
        });
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DestinationResponseDTO>> GetById(int id)
    {
        var destination = await _destinationService.GetByIdAsync(id);
        if (destination == null)
            return NotFound();

        var response = new DestinationResponseDTO
        {
            Id = destination.Id,
            Name = destination.Name,
            Country = destination.Country,
            Description = destination.Description,
            TripCount = destination.Trips?.Count ?? 0
        };
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<DestinationResponseDTO>> Create(CreateDestinationDTO dto)
    {
        var destination = new Destination
        {
            Name = dto.Name,
            Country = dto.Country,
            Description = dto.Description
        };

        var createdDestination = await _destinationService.CreateAsync(destination);
        var response = new DestinationResponseDTO
        {
            Id = createdDestination.Id,
            Name = createdDestination.Name,
            Country = createdDestination.Country,
            Description = createdDestination.Description,
            TripCount = 0
        };
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<DestinationResponseDTO>> Update(int id, UpdateDestinationDTO dto)
    {
        var destination = new Destination
        {
            Id = id,
            Name = dto.Name,
            Country = dto.Country,
            Description = dto.Description
        };

        var updatedDestination = await _destinationService.UpdateAsync(id, destination);
        var response = new DestinationResponseDTO
        {
            Id = updatedDestination.Id,
            Name = updatedDestination.Name,
            Country = updatedDestination.Country,
            Description = updatedDestination.Description,
            TripCount = updatedDestination.Trips?.Count ?? 0
        };
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _destinationService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("country/{country}")]
    public async Task<ActionResult<IEnumerable<DestinationResponseDTO>>> GetByCountry(string country)
    {
        var destinations = await _destinationService.GetDestinationsByCountryAsync(country);
        var response = destinations.Select(d => new DestinationResponseDTO
        {
            Id = d.Id,
            Name = d.Name,
            Country = d.Country,
            Description = d.Description,
            TripCount = d.Trips?.Count ?? 0
        });
        return Ok(response);
    }

    [HttpGet("popular")]
    public async Task<ActionResult<IEnumerable<DestinationResponseDTO>>> GetPopular([FromQuery] int count = 10)
    {
        var destinations = await _destinationService.GetPopularDestinationsAsync(count);
        var response = destinations.Select(d => new DestinationResponseDTO
        {
            Id = d.Id,
            Name = d.Name,
            Country = d.Country,
            Description = d.Description,
            TripCount = d.Trips?.Count ?? 0
        });
        return Ok(response);
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<DestinationResponseDTO>>> Search([FromQuery] string term)
    {
        var destinations = await _destinationService.SearchDestinationsAsync(term);
        var response = destinations.Select(d => new DestinationResponseDTO
        {
            Id = d.Id,
            Name = d.Name,
            Country = d.Country,
            Description = d.Description,
            TripCount = d.Trips?.Count ?? 0
        });
        return Ok(response);
    }

    [HttpGet("countries")]
    public async Task<ActionResult<IEnumerable<string>>> GetAllCountries()
    {
        var countries = await _destinationService.GetAllCountriesAsync();
        return Ok(countries);
    }

    [HttpGet("active")]
    public async Task<ActionResult<IEnumerable<DestinationResponseDTO>>> GetActive()
    {
        var destinations = await _destinationService.GetDestinationsWithActiveTripsAsync();
        var response = destinations.Select(d => new DestinationResponseDTO
        {
            Id = d.Id,
            Name = d.Name,
            Country = d.Country,
            Description = d.Description,
            TripCount = d.Trips?.Count ?? 0
        });
        return Ok(response);
    }

    [HttpGet("sorted")]
    public async Task<ActionResult<IEnumerable<DestinationResponseDTO>>> GetSorted([FromQuery] bool ascending = true)
    {
        var destinations = await _destinationService.GetDestinationsSortedByTripCountAsync(ascending);
        var response = destinations.Select(d => new DestinationResponseDTO
        {
            Id = d.Id,
            Name = d.Name,
            Country = d.Country,
            Description = d.Description,
            TripCount = d.Trips?.Count ?? 0
        });
        return Ok(response);
    }

    [HttpGet("statistics")]
    public async Task<ActionResult<Dictionary<string, int>>> GetStatistics()
    {
        var statistics = await _destinationService.GetDestinationStatisticsAsync();
        return Ok(statistics);
    }
} 