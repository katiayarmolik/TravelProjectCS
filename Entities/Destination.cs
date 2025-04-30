using System.ComponentModel.DataAnnotations;

namespace TravelPlanner.Entities;

public class Destination
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    public string Country { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    
    // Navigation properties
    public List<Trip> Trips { get; set; } = new();
} 