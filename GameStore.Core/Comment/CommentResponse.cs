namespace GameStore.Core.Comment;

public class CommentResponse
{
    public string FormattedBody { get; set; }

    public Guid Id { get; set; }

    public string UserName { get; set; }

    public Guid? ParentCommentId { get; set; }

    public List<CommentResponse> ChildrenComments { get; set; }

    public CommentResponse Map(Comment comment)
    {
        Id = comment.Id;
        UserName = comment.UserName;
        ParentCommentId = comment.ParentCommentId;

        ChildrenComments = comment.ChildrenComments
            .Select(c => new CommentResponse().Map(c))
            .ToList();

        return this;
    }
}