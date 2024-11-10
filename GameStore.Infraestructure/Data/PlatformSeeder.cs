using GameStore.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Infraestructure.Data;

public static class PlatformSeeder
{
    public static void SeedPlatforms(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Platform>().HasData(
            new Platform { Type = "Mobile" },
            new Platform { Type = "Browser" },
            new Platform { Type = "Desktop" },
            new Platform { Type = "Console" });
    }
}