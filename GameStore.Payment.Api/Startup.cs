using System.Text.Json.Serialization;
using GameStore.Payment.Core.GameClient;
using GameStore.Payment.Core.Interfaces;
using GameStore.Payment.Core.Services;
using GameStore.Payment.Core.Services.Payment;
using GameStore.Payment.Infraestructure.Data;

namespace GameStore.Payment.Api;

public class Startup(IConfiguration configuration)
{
    public IConfiguration Configuration => configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSwaggerGen();

        Infraestructure.Dependences.ConfigureServices(Configuration, services);

        services.AddScoped<IPaymentMethodProvider, PaymentMethodProvider>();
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IPaymentProcessorFactory, PaymentProcessorFactory>();

        services.AddHttpClient<IGameServiceClient, GameServiceClient>(client =>
        {
            string baseUrl = Configuration
                .GetValue<string>("GameApplicationUrl")
                ?? throw new InvalidOperationException("GameApplicationUrl not configured.");

            client.BaseAddress = new Uri(baseUrl);
        });

        services.AddControllers()
            .AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                opt.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
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

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}