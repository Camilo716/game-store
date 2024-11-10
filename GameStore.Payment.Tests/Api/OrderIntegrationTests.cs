namespace GameStore.Payment.Tests.Api;

public class OrderIntegrationTests : BaseIntegrationTest
{
    [Fact]
    public async Task GetPaymentMethods_ReturnsSuccess()
    {
        var response = await HttpClient.GetAsync("api/orders/payment-methods");

        Assert.NotNull(response);
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetCart_ReturnsSuccess()
    {
        var response = await HttpClient.GetAsync("api/orders/cart");

        Assert.NotNull(response);
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetOrders_ReturnsSuccess()
    {
        var response = await HttpClient.GetAsync("api/orders");

        Assert.NotNull(response);
        response.EnsureSuccessStatusCode();
    }
}