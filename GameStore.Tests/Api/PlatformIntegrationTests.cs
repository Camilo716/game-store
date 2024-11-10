using System.Net;
using System.Net.Http.Json;
using GameStore.Api.Dtos.PlatformDtos;
using GameStore.Core.Models;
using GameStore.Tests.Api.ClassData;
using GameStore.Tests.Seed;

namespace GameStore.Tests.Api;

public class PlatformIntegrationTests : BaseIntegrationTest
{
    [Fact]
    public async Task GetById_GivenValidId_ReturnsSuccess()
    {
        Guid id = PlatformSeed.GetPlatforms().First().Id;

        var response = await HttpClient.GetAsync($"api/platforms/{id}");

        Assert.NotNull(response);
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetById_GivenInvalidId_ReturnsNotFound()
    {
        var response = await HttpClient.GetAsync("api/platforms/-1");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetAll_WithoutPagination_ReturnsAllPlatforms()
    {
        var response = await HttpClient.GetAsync("api/platforms");

        response.EnsureSuccessStatusCode();
        var platforms = await HttpHelper.GetModelFromHttpResponseAsync<IEnumerable<Platform>>(response);
        Assert.NotNull(platforms);
        Assert.Equal(platforms.Count(), PlatformSeed.GetPlatforms().Count);
    }

    [Fact]
    public async Task Delete_GivenValidId_DeletesPlatform()
    {
        Guid id = PlatformSeed.GetPlatforms().First().Id;

        var response = await HttpClient.DeleteAsync($"api/platforms/{id}");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Equal(PlatformSeed.GetPlatforms().Count - 1, DbContext.Platforms.Count());
    }

    [Fact]
    public async Task Post_GivenValidPlatform_CreatesPlatform()
    {
        PlatformPostRequest validPlatform = new()
        {
            Platform = new SimplePlatformDto() { Type = "Virtual Reality" },
        };

        var response = await HttpClient.PostAsJsonAsync("api/platforms", validPlatform);

        response.EnsureSuccessStatusCode();
        Assert.Equal(PlatformSeed.GetPlatforms().Count + 1, DbContext.Platforms.Count());
    }

    [Fact]
    public async Task Put_GivenValidPlatform_UpdatesPlatform()
    {
        SimplePlatformWithIdDto platformDto = new()
        {
            Id = PlatformSeed.Mobile.Id,
            Type = "Virtual Reality",
        };
        PlatformPutRequest request = new()
        {
            Platform = platformDto,
        };

        var response = await HttpClient.PutAsJsonAsync("api/platforms", request);

        Assert.NotNull(response);
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetGamesByPlatformId_GivenValidId_ReturnsSuccess()
    {
        Guid platformId = PlatformSeed.Console.Id;

        var response = await HttpClient.GetAsync($"api/platforms/{platformId}/games");

        response.EnsureSuccessStatusCode();
        var games = await HttpHelper.GetModelFromHttpResponseAsync<IEnumerable<Game>>(response);
        Assert.NotNull(games);
        var expectedGames = DbContext.Games
            .Where(game => game.Platforms.Select(p => p.Id).Contains(platformId));
        Assert.Equal(expectedGames.Count(), games.Count());
    }

    [Theory]
    [ClassData(typeof(InvalidPlatformsPostRequestTestData))]
    public async Task Post_GivenInvalidPlatform_ReturnsBadRequest(PlatformPostRequest invalidPlatformRequest)
    {
        var response = await HttpClient.PostAsJsonAsync("api/platforms", invalidPlatformRequest);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Theory]
    [ClassData(typeof(InvalidPlatformsPutRequestTestData))]
    public async Task Put_GivenInvalidPlatform_ReturnsBadRequest(PlatformPutRequest invalidPlatformRequest)
    {
        var response = await HttpClient.PutAsJsonAsync("api/platforms", invalidPlatformRequest);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}