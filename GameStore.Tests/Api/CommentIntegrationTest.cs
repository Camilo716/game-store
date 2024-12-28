using System.Net;
using System.Net.Http.Json;
using GameStore.Api.Dtos.CommentDtos;
using GameStore.Tests.Seed;

namespace GameStore.Tests.Api;

public class CommentIntegrationTest : BaseIntegrationTest
{
    [Fact]
    public async Task Post_GivenValidComment_CreatesComment()
    {
        CommentRequest commentRequest = new(
            Comment: new(
                UserName: "UserName",
                Body: "Great Game"),
            ParentId: null);

        string gameKey = GameSeed.GearsOfWar.Key;

        var response = await HttpClient.PostAsJsonAsync($"api/games/{gameKey}/comments", commentRequest);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(CommentSeed.GetComments().Count + 1, DbContext.Comments.Count());
    }

    [Fact]
    public async Task Delete_GivenValidId_DeletesComment()
    {
        Guid id = CommentSeed.GetComments().First().Id;
        string gameKey = GameSeed.GearsOfWar.Key;

        var response = await HttpClient.DeleteAsync($"api/games/comments/{id}");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Equal(1, DbContext.Comments.Where(c => c.Deleted).Count());
    }

    [Fact]
    public async Task GetUserBanDurations_ReturnsSuccess()
    {
        var response = await HttpClient.GetAsync("api/comments/ban/durations");

        Assert.NotNull(response);
        response.EnsureSuccessStatusCode();
    }
}