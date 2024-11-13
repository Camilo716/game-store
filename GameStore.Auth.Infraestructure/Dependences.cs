using GameStore.Auth.Infraestructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameStore.Auth.Infraestructure;

public static class Dependences
{
    public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
    {
        services.AddDbContext<GameStoreAuthDbContext>(opt =>
            opt.UseSqlServer(configuration.GetConnectionString("Default")));

        services
            .AddIdentityCore<IdentityUser>()
            .AddEntityFrameworkStores<GameStoreAuthDbContext>();
    }
}