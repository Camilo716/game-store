using GameStore.Auth.Core.Interfaces;
using GameStore.Auth.Infraestructure.Adapters;
using GameStore.Auth.Infraestructure.Data;
using GameStore.Auth.Infraestructure.Entities;
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
            .AddIdentityCore<User>()
            .AddRoles<Role>()
            .AddEntityFrameworkStores<GameStoreAuthDbContext>()
            .AddUserManager<UserManager<User>>()
            .AddRoleManager<RoleManager<Role>>();

        services.AddScoped<IUserManager, UserManagerIdentityAdapter>();
        services.AddScoped<IRoleManager, RoleManagerIdentityAdapter>();
    }
}