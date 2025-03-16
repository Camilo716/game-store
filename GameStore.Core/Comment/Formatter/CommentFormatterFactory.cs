namespace GameStore.Core.Comment.Formatter;

internal static class CommentFormatterFactory
{
    public static ICommentFormatter Create(CommentType commentType)
    {
        SimpleCommentFormatter simpleCommentFormatter = new();

        return commentType switch
        {
            CommentType.Comment => simpleCommentFormatter,
            CommentType.Reply => new ReplyCommentFormatter(simpleCommentFormatter),
            CommentType.Quote => new QuoteCommentFormatter(simpleCommentFormatter),
            _ => throw new InvalidOperationException("Format not supported."),
        };
    }
}