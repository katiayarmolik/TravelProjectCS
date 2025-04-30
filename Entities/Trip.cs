using System.ComponentModel.DataAnnotations;

namespace TravelPlanner.Entities;

public class Trip
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    public string Description { get; set; } = string.Empty;
    
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public decimal Price { get; set; }
    
    public int PopularityCount { get; set; }
    
    // Navigation properties
    public int DestinationId { get; set; }
    public Destination Destination { get; set; } = null!;
    
    public int DepartureLocationId { get; set; }
    public Destination DepartureLocation { get; set; } = null!;
    
    public int UserId { get; set; }
    public User User { get; set; } = null!;
} 