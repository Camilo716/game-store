namespace GameStore.Core.Comment.Formatter;

internal class ReplyCommentFormatter : ICommentFormatter
{
    public CommentResponse Format(Comment comment)
    {
        return comment.ParentComment is null
            ? throw new InvalidOperationException("Reply must have parent comment.")
            : new CommentResponse(comment)
            {
                FormattedBody = $"[{comment.ParentComment.UserName}], {comment.Body}",
            };
    }
}