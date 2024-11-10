using Newtonsoft.Json;

namespace GameStore.Tests;

public static class HttpHelper
{
    public static async Task<T> GetModelFromHttpResponseAsync<T>(HttpResponseMessage response)
    {
        string stringResponse = await response.Content.ReadAsStringAsync();
        var platforms = JsonConvert.DeserializeObject<T>(stringResponse);
        return platforms;
    }
}