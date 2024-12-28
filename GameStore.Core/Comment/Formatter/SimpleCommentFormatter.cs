namespace GameStore.Core.Comment.Formatter;

internal class SimpleCommentFormatter : ICommentFormatter
{
    public CommentResponse Format(Comment comment)
    {
        return new CommentResponse(comment)
        {
            FormattedBody = comment.Body,
        };
    }
}