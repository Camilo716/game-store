using System.Text;
using GameStore.Auth.Core.Interfaces;
using GameStore.Auth.Infraestructure.Adapters;
using GameStore.Auth.Infraestructure.Data;
using GameStore.Auth.Infraestructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace GameStore.Auth.Infraestructure;

public static class Dependences
{
    public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
    {
        services.AddDbContext<GameStoreAuthDbContext>(opt =>
            opt.UseSqlServer(configuration.GetConnectionString("Default"))
            .EnableSensitiveDataLogging());

        services.AddSingleton(_ => TimeProvider.System);

        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = IdentityConstants.BearerScheme;
            opt.DefaultChallengeScheme = IdentityConstants.BearerScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                LogValidationExceptions = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Jwt:SecretKey"]!)),
            };
        });

        services
            .AddIdentityCore<User>()
            .AddRoles<Role>()
            .AddEntityFrameworkStores<GameStoreAuthDbContext>()
            .AddUserManager<UserManager<User>>()
            .AddRoleManager<RoleManager<Role>>()
            .AddSignInManager<SignInManager<User>>();

        services.AddScoped<IUserManager, UserManagerIdentityAdapter>();
        services.AddScoped<IRoleManager, RoleManagerIdentityAdapter>();
        services.AddScoped<ISignInManager, SignInManagerIdentityAdapter>();
        services.AddScoped<ITokenGenerator, TokenGenerator>();
    }
}