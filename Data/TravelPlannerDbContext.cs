using Microsoft.EntityFrameworkCore;
using TravelPlanner.Entities;

namespace TravelPlanner.Data;

public class TravelPlannerDbContext : DbContext
{
    public TravelPlannerDbContext(DbContextOptions<TravelPlannerDbContext> options) : base(options)
    {
    }

    public DbSet<Trip> Trips { get; set; }
    public DbSet<Destination> Destinations { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Trip configurations
        modelBuilder.Entity<Trip>()
            .HasOne(t => t.Destination)
            .WithMany(d => d.Trips)
            .HasForeignKey(t => t.DestinationId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Trip>()
            .HasOne(t => t.DepartureLocation)
            .WithMany()
            .HasForeignKey(t => t.DepartureLocationId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Trip>()
            .HasOne(t => t.User)
            .WithMany(u => u.Trips)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // User configurations
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();
    }
} 