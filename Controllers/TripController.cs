using Microsoft.AspNetCore.Mvc;
using TravelPlanner.Entities;

namespace TravelPlanner.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TripController : ControllerBase
{
    private readonly ILogger<TripController> _logger;

    public TripController(ILogger<TripController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<Trip>>> GetAllTrips()
    {
        return Ok(new List<Trip>()); // TODO: Implement with service
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Trip>> GetTripById(int id)
    {
        return Ok(new Trip()); // TODO: Implement with service
    }

    [HttpGet("popular")]
    public async Task<ActionResult<List<Trip>>> GetPopularTrips()
    {
        return Ok(new List<Trip>()); // TODO: Implement with service
    }

    [HttpPost]
    public async Task<ActionResult<Trip>> CreateTrip([FromBody] Trip trip)
    {
        return CreatedAtAction(nameof(GetTripById), new { id = trip.Id }, trip); // TODO: Implement with service
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Trip>> UpdateTrip(int id, [FromBody] Trip trip)
    {
        return Ok(trip); // TODO: Implement with service
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTrip(int id)
    {
        return NoContent(); // TODO: Implement with service
    }
} 