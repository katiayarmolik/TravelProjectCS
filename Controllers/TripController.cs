using Microsoft.AspNetCore.Mvc;
using TravelPlanner.DTOs;
using TravelPlanner.Entities;
using TravelPlanner.Services;

namespace TravelPlanner.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TripController : ControllerBase
{
    private readonly ITripService _tripService;

    public TripController(ITripService tripService)
    {
        _tripService = tripService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TripResponseDTO>>> GetAll()
    {
        var trips = await _tripService.GetAllAsync();
        var response = trips.Select(t => new TripResponseDTO
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            StartDate = t.StartDate,
            EndDate = t.EndDate,
            Price = t.Price,
            PopularityCount = t.PopularityCount,
            DestinationId = t.DestinationId,
            DestinationName = t.Destination?.Name ?? string.Empty,
            DepartureLocationId = t.DepartureLocationId,
            DepartureLocationName = t.DepartureLocation?.Name ?? string.Empty,
            UserId = t.UserId,
            UserName = t.User?.Username ?? string.Empty
        });
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TripResponseDTO>> GetById(int id)
    {
        var trip = await _tripService.GetByIdAsync(id);
        if (trip == null)
            return NotFound();

        var response = new TripResponseDTO
        {
            Id = trip.Id,
            Title = trip.Title,
            Description = trip.Description,
            StartDate = trip.StartDate,
            EndDate = trip.EndDate,
            Price = trip.Price,
            PopularityCount = trip.PopularityCount,
            DestinationId = trip.DestinationId,
            DestinationName = trip.Destination?.Name ?? string.Empty,
            DepartureLocationId = trip.DepartureLocationId,
            DepartureLocationName = trip.DepartureLocation?.Name ?? string.Empty,
            UserId = trip.UserId,
            UserName = trip.User?.Username ?? string.Empty
        };
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<TripResponseDTO>> Create(CreateTripDTO dto)
    {
        var trip = new Trip
        {
            Title = dto.Title,
            Description = dto.Description,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            Price = dto.Price,
            DestinationId = dto.DestinationId,
            DepartureLocationId = dto.DepartureLocationId,
            UserId = dto.UserId,
            PopularityCount = 0
        };

        var createdTrip = await _tripService.CreateAsync(trip);
        var response = new TripResponseDTO
        {
            Id = createdTrip.Id,
            Title = createdTrip.Title,
            Description = createdTrip.Description,
            StartDate = createdTrip.StartDate,
            EndDate = createdTrip.EndDate,
            Price = createdTrip.Price,
            PopularityCount = createdTrip.PopularityCount,
            DestinationId = createdTrip.DestinationId,
            DestinationName = createdTrip.Destination?.Name ?? string.Empty,
            DepartureLocationId = createdTrip.DepartureLocationId,
            DepartureLocationName = createdTrip.DepartureLocation?.Name ?? string.Empty,
            UserId = createdTrip.UserId,
            UserName = createdTrip.User?.Username ?? string.Empty
        };
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TripResponseDTO>> Update(int id, UpdateTripDTO dto)
    {
        var trip = new Trip
        {
            Id = id,
            Title = dto.Title,
            Description = dto.Description,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            Price = dto.Price,
            DestinationId = dto.DestinationId,
            DepartureLocationId = dto.DepartureLocationId,
            UserId = dto.UserId
        };

        var updatedTrip = await _tripService.UpdateAsync(id, trip);
        var response = new TripResponseDTO
        {
            Id = updatedTrip.Id,
            Title = updatedTrip.Title,
            Description = updatedTrip.Description,
            StartDate = updatedTrip.StartDate,
            EndDate = updatedTrip.EndDate,
            Price = updatedTrip.Price,
            PopularityCount = updatedTrip.PopularityCount,
            DestinationId = updatedTrip.DestinationId,
            DestinationName = updatedTrip.Destination?.Name ?? string.Empty,
            DepartureLocationId = updatedTrip.DepartureLocationId,
            DepartureLocationName = updatedTrip.DepartureLocation?.Name ?? string.Empty,
            UserId = updatedTrip.UserId,
            UserName = updatedTrip.User?.Username ?? string.Empty
        };
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _tripService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("popular")]
    public async Task<ActionResult<IEnumerable<TripResponseDTO>>> GetPopular([FromQuery] int count = 10)
    {
        var trips = await _tripService.GetPopularTripsAsync(count);
        var response = trips.Select(t => new TripResponseDTO
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            StartDate = t.StartDate,
            EndDate = t.EndDate,
            Price = t.Price,
            PopularityCount = t.PopularityCount,
            DestinationId = t.DestinationId,
            DestinationName = t.Destination?.Name ?? string.Empty,
            DepartureLocationId = t.DepartureLocationId,
            DepartureLocationName = t.DepartureLocation?.Name ?? string.Empty,
            UserId = t.UserId,
            UserName = t.User?.Username ?? string.Empty
        });
        return Ok(response);
    }

    [HttpPost("{id}/increment-popularity")]
    public async Task<ActionResult> IncrementPopularity(int id)
    {
        await _tripService.IncrementPopularityAsync(id);
        return NoContent();
    }

    [HttpGet("date-range")]
    public async Task<ActionResult<IEnumerable<TripResponseDTO>>> GetByDateRange(
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate)
    {
        var trips = await _tripService.GetTripsByDateRangeAsync(startDate, endDate);
        var response = trips.Select(t => new TripResponseDTO
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            StartDate = t.StartDate,
            EndDate = t.EndDate,
            Price = t.Price,
            PopularityCount = t.PopularityCount,
            DestinationId = t.DestinationId,
            DestinationName = t.Destination?.Name ?? string.Empty,
            DepartureLocationId = t.DepartureLocationId,
            DepartureLocationName = t.DepartureLocation?.Name ?? string.Empty,
            UserId = t.UserId,
            UserName = t.User?.Username ?? string.Empty
        });
        return Ok(response);
    }

    [HttpGet("price-range")]
    public async Task<ActionResult<IEnumerable<TripResponseDTO>>> GetByPriceRange(
        [FromQuery] decimal minPrice,
        [FromQuery] decimal maxPrice)
    {
        var trips = await _tripService.GetTripsByPriceRangeAsync(minPrice, maxPrice);
        var response = trips.Select(t => new TripResponseDTO
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            StartDate = t.StartDate,
            EndDate = t.EndDate,
            Price = t.Price,
            PopularityCount = t.PopularityCount,
            DestinationId = t.DestinationId,
            DestinationName = t.Destination?.Name ?? string.Empty,
            DepartureLocationId = t.DepartureLocationId,
            DepartureLocationName = t.DepartureLocation?.Name ?? string.Empty,
            UserId = t.UserId,
            UserName = t.User?.Username ?? string.Empty
        });
        return Ok(response);
    }

    [HttpGet("destination/{destinationId}")]
    public async Task<ActionResult<IEnumerable<TripResponseDTO>>> GetByDestination(int destinationId)
    {
        var trips = await _tripService.GetTripsByDestinationAsync(destinationId);
        var response = trips.Select(t => new TripResponseDTO
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            StartDate = t.StartDate,
            EndDate = t.EndDate,
            Price = t.Price,
            PopularityCount = t.PopularityCount,
            DestinationId = t.DestinationId,
            DestinationName = t.Destination?.Name ?? string.Empty,
            DepartureLocationId = t.DepartureLocationId,
            DepartureLocationName = t.DepartureLocation?.Name ?? string.Empty,
            UserId = t.UserId,
            UserName = t.User?.Username ?? string.Empty
        });
        return Ok(response);
    }

    [HttpGet("departure/{departureLocationId}")]
    public async Task<ActionResult<IEnumerable<TripResponseDTO>>> GetByDepartureLocation(int departureLocationId)
    {
        var trips = await _tripService.GetTripsByDepartureLocationAsync(departureLocationId);
        var response = trips.Select(t => new TripResponseDTO
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            StartDate = t.StartDate,
            EndDate = t.EndDate,
            Price = t.Price,
            PopularityCount = t.PopularityCount,
            DestinationId = t.DestinationId,
            DestinationName = t.Destination?.Name ?? string.Empty,
            DepartureLocationId = t.DepartureLocationId,
            DepartureLocationName = t.DepartureLocation?.Name ?? string.Empty,
            UserId = t.UserId,
            UserName = t.User?.Username ?? string.Empty
        });
        return Ok(response);
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<TripResponseDTO>>> GetByUser(int userId)
    {
        var trips = await _tripService.GetTripsByUserAsync(userId);
        var response = trips.Select(t => new TripResponseDTO
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            StartDate = t.StartDate,
            EndDate = t.EndDate,
            Price = t.Price,
            PopularityCount = t.PopularityCount,
            DestinationId = t.DestinationId,
            DestinationName = t.Destination?.Name ?? string.Empty,
            DepartureLocationId = t.DepartureLocationId,
            DepartureLocationName = t.DepartureLocation?.Name ?? string.Empty,
            UserId = t.UserId,
            UserName = t.User?.Username ?? string.Empty
        });
        return Ok(response);
    }

    [HttpGet("sorted/price")]
    public async Task<ActionResult<IEnumerable<TripResponseDTO>>> GetSortedByPrice([FromQuery] bool ascending = true)
    {
        var trips = await _tripService.GetTripsSortedByPriceAsync(ascending);
        var response = trips.Select(t => new TripResponseDTO
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            StartDate = t.StartDate,
            EndDate = t.EndDate,
            Price = t.Price,
            PopularityCount = t.PopularityCount,
            DestinationId = t.DestinationId,
            DestinationName = t.Destination?.Name ?? string.Empty,
            DepartureLocationId = t.DepartureLocationId,
            DepartureLocationName = t.DepartureLocation?.Name ?? string.Empty,
            UserId = t.UserId,
            UserName = t.User?.Username ?? string.Empty
        });
        return Ok(response);
    }

    [HttpGet("sorted/date")]
    public async Task<ActionResult<IEnumerable<TripResponseDTO>>> GetSortedByDate([FromQuery] bool ascending = true)
    {
        var trips = await _tripService.GetTripsSortedByDateAsync(ascending);
        var response = trips.Select(t => new TripResponseDTO
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            StartDate = t.StartDate,
            EndDate = t.EndDate,
            Price = t.Price,
            PopularityCount = t.PopularityCount,
            DestinationId = t.DestinationId,
            DestinationName = t.Destination?.Name ?? string.Empty,
            DepartureLocationId = t.DepartureLocationId,
            DepartureLocationName = t.DepartureLocation?.Name ?? string.Empty,
            UserId = t.UserId,
            UserName = t.User?.Username ?? string.Empty
        });
        return Ok(response);
    }
} 