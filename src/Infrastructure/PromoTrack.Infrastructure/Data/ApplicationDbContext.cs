using Microsoft.EntityFrameworkCore;
using PromoTrack.Domain;

namespace PromoTrack.Infrastructure.Data;

/// <summary>
/// Represents the database context for the PromoTrack application,
/// acting as the main bridge between our domain entities and the database.
/// </summary>
public class ApplicationDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the ApplicationDbContext with specified options.
    /// </summary>
    /// <param name="options">The options to be used by the DbContext.</param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // Define DbSet properties for each of your domain entities.
    // A DbSet represents a collection of all entities of a specific type
    // in the context, which can be queried from the database.
    // Essentially, this maps our User class to a "Users" table in the database.

    /// <summary>
    /// Gets or sets the DbSet for the User entities.
    /// </summary>
    public DbSet<User> Users { get; set; }

    // As we create more entities (Brand, Campaign, Product, etc.),
    // we will add more DbSet properties here. For example:
    // public DbSet<Brand> Brands { get; set; }
    // public DbSet<Campaign> Campaigns { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Here we can add any specific configurations for our entities,
        // such as setting up composite keys, unique constraints, or indexing.
        // For example, ensuring the Email property on the User entity is unique.
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
    }
}
