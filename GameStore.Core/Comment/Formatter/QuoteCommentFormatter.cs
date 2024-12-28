namespace GameStore.Core.Comment.Formatter;

public class QuoteCommentFormatter : ICommentFormatter
{
    public CommentResponse Format(Comment comment)
    {
        return comment.ParentComment is null
            ? throw new InvalidOperationException("Reply must have parent comment.")
            : new CommentResponse(comment)
            {
                FormattedBody = $"[{comment.ParentComment.Body}], {comment.Body}",
            };
    }
}