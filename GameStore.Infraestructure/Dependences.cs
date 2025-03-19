using System.Text;
using GameStore.Core.Auth;
using GameStore.Infraestructure.Auth;
using GameStore.Infraestructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace GameStore.Infraestructure;

public static class Dependences
{
    public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
    {
        services.AddDbContext<GameStoreDbContext>(opt =>
            opt.UseSqlServer(configuration.GetConnectionString("Default")));

        services.AddScoped<IDatabaseInitializer, DatabaseInitializer>();

        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = IdentityConstants.BearerScheme;
            opt.DefaultChallengeScheme = IdentityConstants.BearerScheme;
            opt.DefaultScheme = IdentityConstants.BearerScheme;
        })
        .AddJwtBearer(IdentityConstants.BearerScheme, options =>
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

        services.AddAuthorization();

        services.AddAuthorizationBuilder()
            .AddPermissionPolicy(Permissions.AddGame)
            .AddPermissionPolicy(Permissions.UpdateGame)
            .AddPermissionPolicy(Permissions.DeleteGame)

            .AddPermissionPolicy(Permissions.ViewGenres)
            .AddPermissionPolicy(Permissions.AddGenre)
            .AddPermissionPolicy(Permissions.UpdateGenre)
            .AddPermissionPolicy(Permissions.DeleteGenre)

            .AddPermissionPolicy(Permissions.ViewPlatforms)
            .AddPermissionPolicy(Permissions.AddPlatform)
            .AddPermissionPolicy(Permissions.UpdatePlatform)
            .AddPermissionPolicy(Permissions.DeletePlatform)

            .AddPermissionPolicy(Permissions.ViewPublishers)
            .AddPermissionPolicy(Permissions.AddPublisher)
            .AddPermissionPolicy(Permissions.UpdatePublisher)
            .AddPermissionPolicy(Permissions.DeletePublisher)

            .AddPermissionPolicy(Permissions.DeleteComment)

            .AddPolicy($"{Policy.NotBanned}", policy =>
                policy.Requirements.Add(new BannedUserRequirement()));

        services.AddScoped<IAuthorizationHandler, PermissionHandler>();
        services.AddScoped<IAuthorizationHandler, BannedUserHandler>();
        services.AddScoped<ITokenValidator, TokenValidator>();
    }

    public static void InitializeDatabase(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
        scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>().Initialize();
    }
}