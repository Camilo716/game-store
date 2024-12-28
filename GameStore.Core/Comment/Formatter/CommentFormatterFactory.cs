namespace GameStore.Core.Comment.Formatter;

internal static class CommentFormatterFactory
{
    public static ICommentFormatter Create(CommentType commentType)
    {
        return commentType switch
        {
            CommentType.Comment => new SimpleCommentFormatter(),
            CommentType.Reply => new ReplyCommentFormatter(),
            CommentType.Quote => new QuoteCommentFormatter(),
            _ => throw new InvalidOperationException("Format not supported."),
        };
    }
}