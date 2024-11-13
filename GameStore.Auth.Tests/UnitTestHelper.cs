using GameStore.Auth.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Auth.Tests;

internal static class UnitTestHelper
{
    internal static GameStoreAuthDbContext GetUnitTestDbContext()
    {
        var options = new DbContextOptionsBuilder<GameStoreAuthDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .EnableSensitiveDataLogging()
            .Options;

        var context = new GameStoreAuthDbContext(options);
        DbSeeder.SeedData(context);

        return context!;
    }
}