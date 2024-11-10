using Microsoft.Extensions.DependencyInjection;

namespace GameStore.Payment.Tests.Api;

public class BaseIntegrationTest : IDisposable
{
    public BaseIntegrationTest()
    {
        Factory = new CustomWebApplicationFactory();
        HttpClient = Factory.CreateClient();

        Scope = Factory.Services.CreateScope();
    }

    protected CustomWebApplicationFactory Factory { get; }

    protected HttpClient HttpClient { get; }

    protected IServiceScope Scope { get; }

    public void Dispose()
    {
        Factory.Dispose();
        HttpClient.Dispose();
        Scope.Dispose();
        GC.SuppressFinalize(this);
    }
}