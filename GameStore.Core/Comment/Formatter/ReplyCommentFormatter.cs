namespace GameStore.Core.Comment.Formatter;

internal class ReplyCommentFormatter(
    SimpleCommentFormatter simpleCommentFormatter)
    : ICommentFormatter
{
    public CommentResponse Format(Comment comment)
    {
        if (comment.ParentComment is null)
        {
            throw new InvalidOperationException("Reply must have parent comment.");
        }

        string formattedBody = simpleCommentFormatter.Format(comment).FormattedBody;

        return new CommentResponse(comment)
        {
            FormattedBody = $"[{comment.ParentComment.UserName}], {formattedBody}",
        };
    }
}