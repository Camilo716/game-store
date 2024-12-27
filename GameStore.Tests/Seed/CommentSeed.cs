using GameStore.Core.Models;

namespace GameStore.Tests.Seed;

public static class CommentSeed
{
    public static Comment PositiveComment => new()
    {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
        Body = "Great Game",
        GameId = GameSeed.GearsOfWar.Id,
    };

    public static List<Comment> GetComments() =>
    [
        PositiveComment,
    ];
}