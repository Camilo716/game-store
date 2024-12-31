using GameStore.Auth.Api;
using GameStore.Auth.Infraestructure.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GameStore.Auth.Tests.Api;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        ConfigureServices(builder);
    }

    private static void ConfigureServices(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            RemoveDbContextServiceRegistration(services);
            ConfigureDbInitializer(services);

            services.AddDbContextPool<GameStoreAuthDbContext>(options =>
            {
                options.UseInMemoryDatabase(Guid.NewGuid().ToString());
                options.EnableSensitiveDataLogging();
            });

            ByPassAuthentication(services);
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

    private static void ByPassAuthentication(IServiceCollection services)
    {
        services
            .AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = TestAuthHandler.AuthScheme;
                x.DefaultChallengeScheme = TestAuthHandler.AuthScheme;
            })
            .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(TestAuthHandler.AuthScheme, _ => { });
    }

    private static void RemoveDbContextServiceRegistration(IServiceCollection services)
    {
        var dbContextDescriptor = services.SingleOrDefault(
            d => d.ServiceType == typeof(DbContextOptions<GameStoreAuthDbContext>))
            ?? throw new InvalidOperationException();

        services.Remove(dbContextDescriptor);
    }
}