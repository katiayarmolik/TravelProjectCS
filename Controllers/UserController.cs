using Microsoft.AspNetCore.Mvc;
using TravelPlanner.DTOs;
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
    public async Task<ActionResult<IEnumerable<UserResponseDTO>>> GetAll()
    {
        var users = await _userService.GetAllAsync();
        var response = users.Select(u => new UserResponseDTO
        {
            Id = u.Id,
            Username = u.Username,
            Email = u.Email,
            FirstName = u.FirstName,
            LastName = u.LastName,
            TripCount = u.Trips.Count
        });
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponseDTO>> GetById(int id)
    {
        var user = await _userService.GetByIdAsync(id);
        if (user == null)
            return NotFound();

        var response = new UserResponseDTO
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            TripCount = user.Trips.Count
        };
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<UserResponseDTO>> Create(CreateUserDTO dto)
    {
        var user = new User
        {
            Username = dto.Username,
            Email = dto.Email,
            PasswordHash = dto.Password, // Note: In a real application, you should hash the password
            FirstName = dto.FirstName,
            LastName = dto.LastName
        };

        var createdUser = await _userService.CreateAsync(user);
        var response = new UserResponseDTO
        {
            Id = createdUser.Id,
            Username = createdUser.Username,
            Email = createdUser.Email,
            FirstName = createdUser.FirstName,
            LastName = createdUser.LastName,
            TripCount = createdUser.Trips.Count
        };
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserResponseDTO>> Update(int id, UpdateUserDTO dto)
    {
        var user = new User
        {
            Id = id,
            Username = dto.Username,
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName
        };

        var updatedUser = await _userService.UpdateAsync(id, user);
        var response = new UserResponseDTO
        {
            Id = updatedUser.Id,
            Username = updatedUser.Username,
            Email = updatedUser.Email,
            FirstName = updatedUser.FirstName,
            LastName = updatedUser.LastName,
            TripCount = updatedUser.Trips.Count
        };
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _userService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("active")]
    public async Task<ActionResult<IEnumerable<UserResponseDTO>>> GetActiveUsers()
    {
        var users = await _userService.GetActiveUsersAsync();
        var response = users.Select(u => new UserResponseDTO
        {
            Id = u.Id,
            Username = u.Username,
            Email = u.Email,
            FirstName = u.FirstName,
            LastName = u.LastName,
            TripCount = u.Trips.Count
        });
        return Ok(response);
    }

    [HttpGet("email/{email}")]
    public async Task<ActionResult<UserResponseDTO>> GetByEmail(string email)
    {
        var user = await _userService.GetByEmailAsync(email);
        if (user == null)
            return NotFound();

        var response = new UserResponseDTO
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            TripCount = user.Trips.Count
        };
        return Ok(response);
    }

    [HttpGet("{id}/trips")]
    public async Task<ActionResult<IEnumerable<Trip>>> GetUserTrips(int id)
    {
        var trips = await _userService.GetUserTripsAsync(id);
        return Ok(trips);
    }

    [HttpGet("sorted")]
    public async Task<ActionResult<IEnumerable<UserResponseDTO>>> GetSortedUsers([FromQuery] string sortBy)
    {
        var users = await _userService.GetSortedUsersAsync(sortBy);
        var response = users.Select(u => new UserResponseDTO
        {
            Id = u.Id,
            Username = u.Username,
            Email = u.Email,
            FirstName = u.FirstName,
            LastName = u.LastName,
            TripCount = u.Trips.Count
        });
        return Ok(response);
    }

    [HttpGet("username/{username}")]
    public async Task<ActionResult<User>> GetByUsername(string username)
    {
        var user = await _userService.GetByUsernameAsync(username);
        if (user == null)
            return NotFound();
        return Ok(user);
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