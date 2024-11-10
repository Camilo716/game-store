using System.Net;
using System.Net.Http.Json;
using GameStore.Api.Dtos.GenreDtos;
using GameStore.Core.Models;
using GameStore.Tests.Api.ClassData;
using GameStore.Tests.Seed;

namespace GameStore.Tests.Api;

public class GenreIntegrationTests : BaseIntegrationTest
{
    [Fact]
    public async Task GetById_GivenValidId_ReturnsSuccess()
    {
        Guid id = GenreSeed.GetGenres().First().Id;

        var response = await HttpClient.GetAsync($"api/genres/{id}");

        Assert.NotNull(response);
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetAll_WithoutPagination_ReturnsAllGenres()
    {
        var response = await HttpClient.GetAsync("api/genres");

        response.EnsureSuccessStatusCode();
        var genres = await HttpHelper.GetModelFromHttpResponseAsync<IEnumerable<Genre>>(response);
        Assert.NotNull(genres);
        Assert.Equal(genres.Count(), GenreSeed.GetGenres().Count);
    }

    [Fact]
    public async Task GetByParentId_GivenValidParentId_ReturnsSuccess()
    {
        Guid parentId = GenreSeed.Action.Id;

        var response = await HttpClient.GetAsync($"api/genres/{parentId}/genres");

        Assert.NotNull(response);
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetGamesByGenreId_GivenValidId_ReturnsSuccess()
    {
        Guid genreId = GenreSeed.Action.Id;

        var response = await HttpClient.GetAsync($"api/genres/{genreId}/games");

        response.EnsureSuccessStatusCode();
        var games = await HttpHelper.GetModelFromHttpResponseAsync<IEnumerable<Game>>(response);
        Assert.NotNull(games);
        var expectedGames = DbContext.Games
            .Where(game => game.Genres.Select(p => p.Id).Contains(genreId));
        Assert.Equal(expectedGames.Count(), games.Count());
    }

    [Fact]
    public async Task Delete_GivenValidId_DeletesGenre()
    {
        Guid id = GenreSeed.GetGenres().First().Id;

        var response = await HttpClient.DeleteAsync($"api/genres/{id}");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Equal(GenreSeed.GetGenres().Count - 1, DbContext.Genres.Count());
    }

    [Fact]
    public async Task Post_GivenValidGenre_CreatesGenre()
    {
        var genre = new GenrePostRequest()
        {
            Genre = new SimpleGenreDto() { Name = "Adventure" },
        };

        var response = await HttpClient.PostAsJsonAsync("api/genres", genre);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(GenreSeed.GetGenres().Count + 1, DbContext.Genres.Count());
    }

    [Fact]
    public async Task Put_GivenValidGenre_UpdatesGenre()
    {
        SimpleGenreWithIdDto genreDto = new()
        {
            Id = GenreSeed.Action.Id,
            Name = "Adventure",
            ParentGenreId = GenreSeed.Action.ParentGenreId,
        };
        GenrePutRequest request = new()
        {
            Genre = genreDto,
        };

        var response = await HttpClient.PutAsJsonAsync("api/genres", request);

        Assert.NotNull(response);
        response.EnsureSuccessStatusCode();
    }

    [Theory]
    [ClassData(typeof(InvalidGenresPostRequestTestData))]
    public async Task Post_GivenInvalidGenre_ReturnsBadRequest(GenrePostRequest invalidGenreRequest)
    {
        var response = await HttpClient.PostAsJsonAsync("api/genres", invalidGenreRequest);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Theory]
    [ClassData(typeof(InvalidGenresPutRequestTestData))]
    public async Task Put_GivenInvalidGenre_ReturnsBadRequest(GenrePutRequest invalidGenreRequest)
    {
        var response = await HttpClient.PutAsJsonAsync("api/genres", invalidGenreRequest);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}