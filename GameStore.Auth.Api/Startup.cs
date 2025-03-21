using System.Text.Json.Serialization;
using AutoMapper;
using GameStore.Auth.Core.Date;
using GameStore.Auth.Core.Privilege;
using GameStore.Auth.Core.Role;
using GameStore.Auth.Core.UnitOfWork;
using GameStore.Auth.Core.User;
using GameStore.Auth.Core.User.Ban;
using GameStore.Auth.Core.User.Ban.Expiration;
using GameStore.Auth.Infraestructure.Data;

namespace GameStore.Auth.Api;
public class Startup(IConfiguration configuration)
{
    public IConfiguration Configuration => configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSwaggerGen();
        services.AddAutoMapper(typeof(Infraestructure.AutoMapperProfile));

        Infraestructure.Dependences.ConfigureServices(Configuration, services);

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped<IPrivilegeService, PrivilegeService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserBanService, UserBanService>();
        services.AddScoped<IExpirationCalculator, ExpirationCalculator>();
        services.AddScoped<IRoleService, RoleService>();

        services.AddEndpointsApiExplorer();

        services.AddControllers()
            .AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            Infraestructure.Dependences.InitializeDatabase(app);
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors(opt => opt
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}