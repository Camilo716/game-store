using GameStore.Tests.Seed;

namespace GameStore.Tests.Api;

public class TotalGamesCountMiddlewareTests : BaseIntegrationTest
{
    [Fact]
    public async Task HttpResponses_ShouldContainTotalGamesCountHeader()
    {
        const string countHeader = "x-total-numbers-of-games";
        int currentGamesInDb = GameSeed.GetGames().Count;

        var response = await HttpClient.GetAsync("api/games");

        response.EnsureSuccessStatusCode();
        Assert.Contains(countHeader, response.Headers.Select(h => h.Key));
        response.Headers.TryGetValues(countHeader, out var values);
        Assert.Equal(currentGamesInDb.ToString(), values?.FirstOrDefault());
    }
}