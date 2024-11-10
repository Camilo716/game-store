using GameStore.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameStore.Infraestructure;

public static class Dependences
{
    public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
    {
        services.AddDbContext<GameStoreDbContext>(opt =>
            opt.UseSqlServer(configuration.GetConnectionString("Default")));
    }
}