using GameStore.Payment.Api;
using GameStore.Payment.Core.GameClient;
using GameStore.Payment.Infraestructure.Data;
using GameStore.Payment.Tests.Api.Mocks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameStore.Payment.Tests.Api;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        ConfigureAppConfiguration(builder);
        ConfigureService(builder);
    }

    private static void ConfigureAppConfiguration(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((context, config) =>
        {
            var testConfig = new Dictionary<string, string?>
            {
                { "GameApplicationUrl", "https://mockurl.com" },
            };

            config.AddInMemoryCollection(testConfig);
        });
    }

    private static void ConfigureService(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            RemoveDbContextServiceRegistration(services);
            ConfigureDbInitializer(services);

            services.AddDbContextPool<GameStorePaymentDbContext>(options =>
            {
                options.UseInMemoryDatabase(Guid.NewGuid().ToString());
                options.EnableSensitiveDataLogging();
            });

            RemoveHttpClientServiceRegistration(services);
            services.AddHttpClient<IGameServiceClient, MockGameServiceClient>();
        });
    }

    private static void ConfigureDbInitializer(IServiceCollection services)
    {
        var dbInitializerDescriptor = services.SingleOrDefault(
            d => d.ServiceType == typeof(IDatabaseInitializer))
            ?? throw new InvalidOperationException();

        services.Remove(dbInitializerDescriptor);
        services.AddSingleton<IDatabaseInitializer, DummyDatabaseInitializer>();
    }

    private static void RemoveDbContextServiceRegistration(IServiceCollection services)
    {
        var dbContextDescriptor = services.SingleOrDefault(
            d => d.ServiceType == typeof(DbContextOptions<GameStorePaymentDbContext>))
            ?? throw new InvalidOperationException();

        services.Remove(dbContextDescriptor);
    }

    private static void RemoveHttpClientServiceRegistration(IServiceCollection services)
    {
        var httpClientDescriptor = services.SingleOrDefault(
            d => d.ServiceType == typeof(IGameServiceClient))
            ?? throw new InvalidOperationException();

        services.Remove(httpClientDescriptor);
    }
}