namespace GameStore.Core.Comment.Formatter;

public class CommentFormatter : ICommentFormatter
{
    public CommentResponse Format(Comment comment)
    {
        return CommentFormatterFactory
            .Create(comment.Type)
            .Format(comment);
    }
}