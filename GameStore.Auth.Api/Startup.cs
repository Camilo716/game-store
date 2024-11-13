using System.Text.Json.Serialization;

namespace GameStore.Auth.Api;
public class Startup(IConfiguration configuration)
{
    public IConfiguration Configuration => configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSwaggerGen();

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
                .AllowAnyHeader()
                .WithExposedHeaders("x-total-numbers-of-games"));

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}