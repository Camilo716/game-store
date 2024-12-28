namespace GameStore.Core.Comment.Formatter;

public class QuoteCommentFormatter(
    SimpleCommentFormatter simpleCommentFormatter) : ICommentFormatter
{
    public CommentResponse Format(Comment comment)
    {
        if (comment.ParentComment is null)
        {
            throw new InvalidOperationException("Reply must have parent comment.");
        }

        string quoteBody = simpleCommentFormatter.Format(comment.ParentComment).FormattedBody;
        string formattedBody = simpleCommentFormatter.Format(comment).FormattedBody;

        return new CommentResponse(comment)
        {
            FormattedBody = $"[{quoteBody}], {formattedBody}",
        };
    }
}