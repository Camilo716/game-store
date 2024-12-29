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

        var response = new CommentResponse().Map(comment);
        response.FormattedBody = $"[{comment.ParentComment.UserName}], {formattedBody}";
        return response;
    }
}