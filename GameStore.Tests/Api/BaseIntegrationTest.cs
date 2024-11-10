using GameStore.Infraestructure.Data;
using GameStore.Tests.Seed;
using Microsoft.Extensions.DependencyInjection;

namespace GameStore.Tests.Api;

public class BaseIntegrationTest : IDisposable
{
    public BaseIntegrationTest()
    {
        Factory = new CustomWebApplicationFactory();
        HttpClient = Factory.CreateClient();

        Scope = Factory.Services.CreateScope();
        DbContext = Scope.ServiceProvider.GetRequiredService<GameStoreDbContext>();
        DbSeeder.SeedData(DbContext);
    }

    protected CustomWebApplicationFactory Factory { get; }

    protected HttpClient HttpClient { get; }

    protected GameStoreDbContext DbContext { get; }

    protected IServiceScope Scope { get; }

    public void Dispose()
    {
        Factory.Dispose();
        HttpClient.Dispose();
        DbContext.Dispose();
        Scope.Dispose();
        GC.SuppressFinalize(this);
    }
}