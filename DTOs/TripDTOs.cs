using System.ComponentModel.DataAnnotations;

namespace TravelPlanner.DTOs;

public class CreateTripDTO
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    public string Description { get; set; } = string.Empty;
    
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public decimal Price { get; set; }
    
    public int DestinationId { get; set; }
    public int DepartureLocationId { get; set; }
    public int UserId { get; set; }
}

public class UpdateTripDTO
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    public string Description { get; set; } = string.Empty;
    
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public decimal Price { get; set; }
    
    public int DestinationId { get; set; }
    public int DepartureLocationId { get; set; }
    public int UserId { get; set; }
}

public class TripResponseDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Price { get; set; }
    public int PopularityCount { get; set; }
    public int DestinationId { get; set; }
    public string DestinationName { get; set; } = string.Empty;
    public int DepartureLocationId { get; set; }
    public string DepartureLocationName { get; set; } = string.Empty;
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
} 