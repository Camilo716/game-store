using GameStore.Core.Comment;

namespace GameStore.Tests.Seed;

public static class CommentSeed
{
    public static Comment PositiveComment => new()
    {
        UserName = "User123",
        Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
        Body = "Great Game",
        GameId = GameSeed.GearsOfWar.Id,
    };

    public static Comment ReplyComment => new()
    {
        UserName = "AnotherUser123",
        Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
        Body = "I Agree",
        GameId = GameSeed.GearsOfWar.Id,
        ParentCommentId = PositiveComment.Id,
    };

    public static List<Comment> GetComments() =>
    [
        PositiveComment,
        ReplyComment,
    ];
}