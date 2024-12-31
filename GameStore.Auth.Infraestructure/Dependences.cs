using System.Text;
using GameStore.Auth.Core.Config;
using GameStore.Auth.Core.Role;
using GameStore.Auth.Core.Token;
using GameStore.Auth.Core.User;
using GameStore.Auth.Core.User.Login;
using GameStore.Auth.Infraestructure.Adapters;
using GameStore.Auth.Infraestructure.Data;
using GameStore.Auth.Infraestructure.Entities;
using GameStore.Auth.Infraestructure.Handlers;
using GameStore.Auth.Infraestructure.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
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

        services.AddScoped<IAuthorizationHandler, PermissionHandler>();

        services.AddAuthorizationBuilder()
            .AddPermissionPolicy(Permissions.ViewRoles)
            .AddPermissionPolicy(Permissions.AddRole)
            .AddPermissionPolicy(Permissions.DeleteRole)
            .AddPermissionPolicy(Permissions.UpdateRole)

            .AddPermissionPolicy(Permissions.ViewUsers)
            .AddPermissionPolicy(Permissions.AddUser)
            .AddPermissionPolicy(Permissions.DeleteUser)
            .AddPermissionPolicy(Permissions.UpdateUser)
            .AddPermissionPolicy(Permissions.BanUser);

        services.AddAuthorization();

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
        services.AddScoped<ITokenValidator, TokenValidator>();
    }

    public static void InitializeDatabase(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
        scope.ServiceProvider.GetRequiredService<GameStoreAuthDbContext>().Database.Migrate();
    }
}