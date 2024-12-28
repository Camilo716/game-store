namespace GameStore.Core.Comment;

public class CommentResponse(
    Comment comment)
{
    public Guid Id { get; set; } = comment.Id;

    public string FormattedBody { get; set; }
}