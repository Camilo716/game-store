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
}