using System.Net;
using System.Net.Http.Json;
using GameStore.Api.Dtos.PublisherDtos;
using GameStore.Core.Game;
using GameStore.Core.Publisher;
using GameStore.Tests.Api.ClassData;
using GameStore.Tests.Seed;

namespace GameStore.Tests.Api;

public class PublisherIntegrationTests : BaseIntegrationTest
{
    [Fact]
    public async Task GetById_GivenValidId_ReturnsSuccess()
    {
        Guid id = PublisherSeed.GetPublishers().First().Id;

        var response = await HttpClient.GetAsync($"api/publishers/{id}");

        Assert.NotNull(response);
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetById_GivenInvalidId_ReturnsNotFound()
    {
        var response = await HttpClient.GetAsync("api/publishers/-1");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetAll_WithoutPagination_ReturnsAllPublishers()
    {
        var response = await HttpClient.GetAsync("api/publishers");

        response.EnsureSuccessStatusCode();
        var publishers = await HttpHelper.GetModelFromHttpResponseAsync<IEnumerable<Publisher>>(response);
        Assert.NotNull(publishers);
        Assert.Equal(publishers.Count(), PublisherSeed.GetPublishers().Count);
    }

    [Fact]
    public async Task Delete_GivenValidId_DeletesPublisher()
    {
        Guid id = PublisherSeed.GetPublishers().First().Id;

        var response = await HttpClient.DeleteAsync($"api/publishers/{id}");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Equal(PublisherSeed.GetPublishers().Count - 1, DbContext.Publishers.Count());
    }

    [Fact]
    public async Task Post_GivenValidPublisher_CreatesPublisher()
    {
        PublisherPostRequest validPublisher = new()
        {
            Publisher = new SimplePublisherDto() { CompanyName = "Activision 2.0" },
        };

        var response = await HttpClient.PostAsJsonAsync("api/publishers", validPublisher);

        response.EnsureSuccessStatusCode();
        Assert.Equal(PublisherSeed.GetPublishers().Count + 1, DbContext.Publishers.Count());
    }

    [Fact]
    public async Task Put_GivenValidPublisher_UpdatesPublisher()
    {
        SimplePublisherWithIdDto publisherDto = new()
        {
            Id = PublisherSeed.Activision.Id,
            CompanyName = "Activision 2.0",
        };
        PublisherPutRequest request = new()
        {
            Publisher = publisherDto,
        };

        var response = await HttpClient.PutAsJsonAsync("api/publishers", request);

        Assert.NotNull(response);
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetGamesByPublisherId_GivenValidId_ReturnsSuccess()
    {
        Guid publisherId = PublisherSeed.Activision.Id;

        var response = await HttpClient.GetAsync($"api/publishers/{publisherId}/games");

        response.EnsureSuccessStatusCode();
        var games = await HttpHelper.GetModelFromHttpResponseAsync<IEnumerable<Game>>(response);
        Assert.NotNull(games);
        var expectedGames = DbContext.Games
            .Where(game => game.Publisher.Id == publisherId);
        Assert.Equal(expectedGames.Count(), games.Count());
    }

    [Theory]
    [ClassData(typeof(InvalidPublishersPostRequestTestData))]
    public async Task Post_GivenInvalidPublisher_ReturnsBadRequest(PublisherPostRequest invalidPublisherRequest)
    {
        var response = await HttpClient.PostAsJsonAsync("api/publishers", invalidPublisherRequest);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Theory]
    [ClassData(typeof(InvalidPublishersPutRequestTestData))]
    public async Task Put_GivenInvalidPublisher_ReturnsBadRequest(PublisherPutRequest invalidPublisherRequest)
    {
        var response = await HttpClient.PutAsJsonAsync("api/publishers", invalidPublisherRequest);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}