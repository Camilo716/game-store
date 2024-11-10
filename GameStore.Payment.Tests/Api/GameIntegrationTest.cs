using System.Net.Http.Json;
using GameStore.Payment.Tests.Seed;

namespace GameStore.Payment.Tests.Api;

public class GameIntegrationTest : BaseIntegrationTest
{
    [Fact]
    public async Task BuyGame_ReturnsSuccess()
    {
        var gameId = OrderGameSeed.OrderGame1.ProductId;

        var response = await HttpClient.PostAsJsonAsync($"api/games/{gameId}/buy", new object() { });

        Assert.NotNull(response);
        response.EnsureSuccessStatusCode();
    }
}