using System.ComponentModel.DataAnnotations;

namespace TravelPlanner.DTOs;

public class CreateDestinationDTO
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    public string Country { get; set; } = string.Empty;
    
    public string? Description { get; set; }
}

public class UpdateDestinationDTO
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    public string Country { get; set; } = string.Empty;
    
    public string? Description { get; set; }
}

public class DestinationResponseDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int TripCount { get; set; }
} 