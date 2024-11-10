using GameStore.Payment.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Payment.Tests;

internal static class UnitTestHelper
{
    internal static GameStorePaymentDbContext GetUnitTestDbContext()
    {
        var options = new DbContextOptionsBuilder<GameStorePaymentDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .EnableSensitiveDataLogging()
            .Options;

        var context = new GameStorePaymentDbContext(options);
        DbSeeder.SeedData(context);

        return context!;
    }
}