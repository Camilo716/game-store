using System.Net.Http.Json;
using GameStore.Payment.Core.Dtos;
using GameStore.Payment.Tests.Seed;

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

    [Fact]
    public async Task GetOrderDetails_ReturnsSuccess()
    {
        Guid orderId = OrderSeed.OpenedOrder.Id;

        var response = await HttpClient.GetAsync($"api/orders/{orderId}/details");

        Assert.NotNull(response);
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task DeleteGameFromCart_ReturnsSuccess()
    {
        const string gameKey = "mockGameKey";

        var response = await HttpClient.DeleteAsync($"api/orders/cart/{gameKey}");

        Assert.NotNull(response);
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task PayOrder_ReturnsSuccess()
    {
        PaymentRequest request = new()
        {
            PaymentMethod = Payment.Core.Enums.PaymentMethod.Bank,
        };

        var response = await HttpClient.PostAsJsonAsync($"api/orders/payment", request);

        Assert.NotNull(response);
        response.EnsureSuccessStatusCode();
    }
}