namespace GameStore.Core.Comment;

public class CommentResponse(
    Comment comment)
{
    public string FormattedBody { get; set; }

    public Guid Id { get; set; } = comment.Id;

    public string UserName { get; set; } = comment.UserName;

    public Guid? ParentCommentId { get; set; } = comment.ParentCommentId;

    public List<CommentResponse> ChildrenComments { get; set; } = comment.ChildrenComments
        .Select(c => new CommentResponse(c))
        .ToList();
}