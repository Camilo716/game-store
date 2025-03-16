using GameStore.Payment.Infraestructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameStore.Payment.Infraestructure;

public static class Dependences
{
    public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
    {
        services.AddDbContext<GameStorePaymentDbContext>(opt =>
            opt.UseSqlServer(configuration.GetConnectionString("Default")));

        services.AddSingleton<IDatabaseInitializer, DatabaseInitializer>();
    }

    public static void InitializeDatabase(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
        scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>().Initialize();
    }
}