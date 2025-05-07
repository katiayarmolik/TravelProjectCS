using Microsoft.AspNetCore.Mvc;
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
    public async Task<ActionResult<IEnumerable<Trip>>> GetAll()
    {
        var trips = await _tripService.GetAllAsync();
        return Ok(trips);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Trip>> GetById(int id)
    {
        var trip = await _tripService.GetByIdAsync(id);
        if (trip == null)
            return NotFound();
        return Ok(trip);
    }

    [HttpPost]
    public async Task<ActionResult<Trip>> Create(Trip trip)
    {
        var createdTrip = await _tripService.CreateAsync(trip);
        return CreatedAtAction(nameof(GetById), new { id = createdTrip.Id }, createdTrip);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Trip>> Update(int id, Trip trip)
    {
        var updatedTrip = await _tripService.UpdateAsync(id, trip);
        return Ok(updatedTrip);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _tripService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("popular")]
    public async Task<ActionResult<IEnumerable<Trip>>> GetPopular([FromQuery] int count = 10)
    {
        var trips = await _tripService.GetPopularTripsAsync(count);
        return Ok(trips);
    }

    [HttpPost("{id}/increment-popularity")]
    public async Task<ActionResult> IncrementPopularity(int id)
    {
        await _tripService.IncrementPopularityAsync(id);
        return NoContent();
    }

    [HttpGet("date-range")]
    public async Task<ActionResult<IEnumerable<Trip>>> GetByDateRange(
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate)
    {
        var trips = await _tripService.GetTripsByDateRangeAsync(startDate, endDate);
        return Ok(trips);
    }

    [HttpGet("price-range")]
    public async Task<ActionResult<IEnumerable<Trip>>> GetByPriceRange(
        [FromQuery] decimal minPrice,
        [FromQuery] decimal maxPrice)
    {
        var trips = await _tripService.GetTripsByPriceRangeAsync(minPrice, maxPrice);
        return Ok(trips);
    }

    [HttpGet("destination/{destinationId}")]
    public async Task<ActionResult<IEnumerable<Trip>>> GetByDestination(int destinationId)
    {
        var trips = await _tripService.GetTripsByDestinationAsync(destinationId);
        return Ok(trips);
    }

    [HttpGet("departure/{departureLocationId}")]
    public async Task<ActionResult<IEnumerable<Trip>>> GetByDepartureLocation(int departureLocationId)
    {
        var trips = await _tripService.GetTripsByDepartureLocationAsync(departureLocationId);
        return Ok(trips);
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<Trip>>> GetByUser(int userId)
    {
        var trips = await _tripService.GetTripsByUserAsync(userId);
        return Ok(trips);
    }

    [HttpGet("sorted/price")]
    public async Task<ActionResult<IEnumerable<Trip>>> GetSortedByPrice([FromQuery] bool ascending = true)
    {
        var trips = await _tripService.GetTripsSortedByPriceAsync(ascending);
        return Ok(trips);
    }

    [HttpGet("sorted/date")]
    public async Task<ActionResult<IEnumerable<Trip>>> GetSortedByDate([FromQuery] bool ascending = true)
    {
        var trips = await _tripService.GetTripsSortedByDateAsync(ascending);
        return Ok(trips);
    }
} 