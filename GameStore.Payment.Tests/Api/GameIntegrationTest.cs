using System.Net.Http.Json;

namespace GameStore.Payment.Tests.Api;

public class GameIntegrationTest : BaseIntegrationTest
{
    [Fact]
    public async Task BuyGame_ReturnsSuccess()
    {
        const string gameKey = "mockGameKey";

        var response = await HttpClient.PostAsJsonAsync($"api/games/{gameKey}/buy", new object() { });

        Assert.NotNull(response);
        response.EnsureSuccessStatusCode();
    }
}