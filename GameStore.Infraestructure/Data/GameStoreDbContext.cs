using GameStore.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Infraestructure.Data;

public class GameStoreDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Platform> Platforms { get; set; }

    public DbSet<Genre> Genres { get; set; }

    public DbSet<Game> Games { get; set; }

    public DbSet<Publisher> Publishers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>()
            .HasIndex(g => g.Key)
            .IsUnique();

        modelBuilder.Entity<Platform>()
            .HasIndex(p => p.Type)
            .IsUnique();

        modelBuilder.Entity<Genre>()
            .HasIndex(g => g.Name)
            .IsUnique();

        GenreSeeder.SeedGenres(modelBuilder);
        PlatformSeeder.SeedPlatforms(modelBuilder);
    }
}