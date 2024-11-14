using GameStore.Auth.Infraestructure.Data;
/* using GameStore.Auth.Infraestructure.Entities;
using GameStore.Auth.Tests.Seed;
using Microsoft.AspNetCore.Identity; */
using Microsoft.Extensions.DependencyInjection;

namespace GameStore.Auth.Tests.Api;

public class BaseIntegrationTest : IDisposable
{
    public BaseIntegrationTest()
    {
        Factory = new CustomWebApplicationFactory();
        HttpClient = Factory.CreateClient();

        Scope = Factory.Services.CreateScope();
        DbContext = Scope.ServiceProvider.GetRequiredService<GameStoreAuthDbContext>();
        DbSeeder.SeedData(DbContext);
    }

    protected CustomWebApplicationFactory Factory { get; }

    protected HttpClient HttpClient { get; }

    protected GameStoreAuthDbContext DbContext { get; }

    protected IServiceScope Scope { get; }

    public void Dispose()
    {
        Factory.Dispose();
        HttpClient.Dispose();
        Scope.Dispose();
        GC.SuppressFinalize(this);
    }
}