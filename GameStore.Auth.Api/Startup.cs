using System.Text.Json.Serialization;
using AutoMapper;
using GameStore.Auth.Core.Interfaces;
using GameStore.Auth.Core.Services;
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

        services.AddScoped<IPrivilegeService, PrivilegeService>();
        services.AddScoped<IUserService, UserService>();
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