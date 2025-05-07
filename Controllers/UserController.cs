using Microsoft.AspNetCore.Mvc;
using TravelPlanner.Entities;
using TravelPlanner.Services;

namespace TravelPlanner.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAll()
    {
        var users = await _userService.GetAllAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetById(int id)
    {
        var user = await _userService.GetByIdAsync(id);
        if (user == null)
            return NotFound();
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<User>> Create(User user)
    {
        var createdUser = await _userService.CreateAsync(user);
        return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createdUser);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<User>> Update(int id, User user)
    {
        var updatedUser = await _userService.UpdateAsync(id, user);
        return Ok(updatedUser);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _userService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("email/{email}")]
    public async Task<ActionResult<User>> GetByEmail(string email)
    {
        var user = await _userService.GetByEmailAsync(email);
        if (user == null)
            return NotFound();
        return Ok(user);
    }

    [HttpGet("username/{username}")]
    public async Task<ActionResult<User>> GetByUsername(string username)
    {
        var user = await _userService.GetByUsernameAsync(username);
        if (user == null)
            return NotFound();
        return Ok(user);
    }

    [HttpGet("{id}/trips")]
    public async Task<ActionResult<IEnumerable<Trip>>> GetUserTrips(int id)
    {
        var trips = await _userService.GetUserTripsAsync(id);
        return Ok(trips);
    }

    [HttpGet("active")]
    public async Task<ActionResult<IEnumerable<User>>> GetActive()
    {
        var users = await _userService.GetActiveUsersAsync();
        return Ok(users);
    }

    [HttpGet("sorted")]
    public async Task<ActionResult<IEnumerable<User>>> GetSorted([FromQuery] int count = 10)
    {
        var users = await _userService.GetUsersByTripCountAsync(count);
        return Ok(users);
    }

    [HttpGet("statistics")]
    public async Task<ActionResult<Dictionary<string, int>>> GetStatistics()
    {
        var statistics = await _userService.GetUserStatisticsAsync();
        return Ok(statistics);
    }

    [HttpPut("{id}/password")]
    public async Task<ActionResult> UpdatePassword(int id, [FromBody] string newPassword)
    {
        await _userService.UpdateUserPasswordAsync(id, newPassword);
        return NoContent();
    }

    [HttpPost("validate")]
    public async Task<ActionResult<bool>> ValidateCredentials([FromBody] UserCredentials credentials)
    {
        var isValid = await _userService.ValidateUserCredentialsAsync(credentials.Username, credentials.Password);
        return Ok(isValid);
    }

    [HttpGet("{id}/upcoming-trips")]
    public async Task<ActionResult<IEnumerable<Trip>>> GetUpcomingTrips(int id)
    {
        var trips = await _userService.GetUserUpcomingTripsAsync(id);
        return Ok(trips);
    }

    [HttpGet("{id}/past-trips")]
    public async Task<ActionResult<IEnumerable<Trip>>> GetPastTrips(int id)
    {
        var trips = await _userService.GetUserPastTripsAsync(id);
        return Ok(trips);
    }
}

public class UserCredentials
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
} 