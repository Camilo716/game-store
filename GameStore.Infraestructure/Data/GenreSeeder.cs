using GameStore.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Infraestructure.Data;

public static class GenreSeeder
{
    public static void SeedGenres(ModelBuilder modelBuilder)
    {
        var strategyId = Guid.NewGuid();
        var rpgId = Guid.NewGuid();
        var sportsId = Guid.NewGuid();
        var actionId = Guid.NewGuid();
        var adventureId = Guid.NewGuid();
        var puzzleSkillId = Guid.NewGuid();

        // Seed main genres
        modelBuilder.Entity<Genre>().HasData(
            new Genre { Id = strategyId, Name = "Strategy" },
            new Genre { Id = rpgId, Name = "RPG" },
            new Genre { Id = sportsId, Name = "Sports" },
            new Genre { Id = actionId, Name = "Action" },
            new Genre { Id = adventureId, Name = "Adventure" },
            new Genre { Id = puzzleSkillId, Name = "Puzzle & Skill" });

        // Subgenres
        modelBuilder.Entity<Genre>().HasData(
            new Genre { Id = Guid.NewGuid(), Name = "RTS", ParentGenreId = strategyId },
            new Genre { Id = Guid.NewGuid(), Name = "TBS", ParentGenreId = strategyId },
            new Genre { Id = Guid.NewGuid(), Name = "Races", ParentGenreId = sportsId },
            new Genre { Id = Guid.NewGuid(), Name = "Rally", ParentGenreId = sportsId },
            new Genre { Id = Guid.NewGuid(), Name = "Formula", ParentGenreId = sportsId },
            new Genre { Id = Guid.NewGuid(), Name = "Off-road", ParentGenreId = sportsId },
            new Genre { Id = Guid.NewGuid(), Name = "FPS", ParentGenreId = actionId },
            new Genre { Id = Guid.NewGuid(), Name = "TPS", ParentGenreId = actionId });
    }
}