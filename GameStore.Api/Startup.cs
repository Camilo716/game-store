using System.Text.Json.Serialization;
using AutoMapper;
using GameStore.Core.Interfaces;
using GameStore.Core.Services;
using GameStore.Infraestructure.Data;
using GameStore.Logging;
using Microsoft.AspNetCore.HttpLogging;

namespace GameStore.Api;
public class Startup(IConfiguration configuration)
{
    public IConfiguration Configuration => configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddHttpLogging(opt =>
            opt.LoggingFields = HttpLoggingFields.RequestPropertiesAndHeaders);

        Infraestructure.Dependences.ConfigureServices(Configuration, services);

        services.AddSwaggerGen();
        services.AddAutoMapper(typeof(AutoMapperProfile));

        services.AddScoped<IPlatformService, PlatformService>();
        services.AddScoped<IGenreService, GenreService>();
        services.AddScoped<IPublisherService, PublisherService>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<IGameService, GameService>();
        services.AddScoped<IGameFileService, GameTextFileService>();

        services.AddControllers()
            .AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
    {
        loggerFactory.AddFileLoggingProvider(
            filePath: Path.Combine(Directory.GetCurrentDirectory(), "logs"));

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<ExceptionHandlerMiddleware>();
        app.UseMiddleware<TotalGamesCountMiddleware>();

        app.UseHttpsRedirection();
        app.UseHttpLogging();

        app.UseRouting();

        app.UseCors(opt => opt
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders("x-total-numbers-of-games"));

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}