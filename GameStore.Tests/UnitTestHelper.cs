using GameStore.Infraestructure.Data;
using GameStore.Tests.Seed;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Tests;

internal static class UnitTestHelper
{
    internal static GameStoreDbContext GetUnitTestDbContext()
    {
        var options = new DbContextOptionsBuilder<GameStoreDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .EnableSensitiveDataLogging()
            .Options;

        var context = new GameStoreDbContext(options);
        DbSeeder.SeedData(context);

        return context!;
    }
}