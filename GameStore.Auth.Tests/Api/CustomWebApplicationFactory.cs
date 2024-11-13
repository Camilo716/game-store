using GameStore.Auth.Api;
using GameStore.Auth.Infraestructure.Data;
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

            services.AddDbContextPool<GameStoreAuthDbContext>(options =>
            {
                options.UseInMemoryDatabase(Guid.NewGuid().ToString());
                options.EnableSensitiveDataLogging();
            });
        });
    }

    private static void RemoveDbContextServiceRegistration(IServiceCollection services)
    {
        var dbContextDescriptor = services.SingleOrDefault(
            d => d.ServiceType == typeof(DbContextOptions<GameStoreAuthDbContext>))
            ?? throw new InvalidOperationException();

        services.Remove(dbContextDescriptor);
    }
}