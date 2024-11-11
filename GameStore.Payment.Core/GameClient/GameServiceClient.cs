using System.Text.Json;

namespace GameStore.Payment.Core.GameClient;

public class GameServiceClient(
    HttpClient httpClient)
    : IGameServiceClient
{
    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
    };

    private HttpClient HttpClient => httpClient;

    public async Task<Game> GetByKeyAsync(string key)
    {
        var response = await HttpClient.GetAsync($"api/games/{key}");

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Game>(content, _jsonOptions);
    }
}