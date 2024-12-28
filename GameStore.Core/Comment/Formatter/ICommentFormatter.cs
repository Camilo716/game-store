namespace GameStore.Core.Comment.Formatter;

public interface ICommentFormatter
{
    CommentResponse Format(Comment comment);
}