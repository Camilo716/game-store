namespace GameStore.Core.Comment.Formatter;

public class CommentFormatter : ICommentFormatter
{
    public CommentResponse Format(Comment comment)
    {
        ICommentFormatter formatter = CommentFormatterFactory.Create(comment.Type);

        CommentResponse formattedComment = formatter.Format(comment);

        if (comment.ChildrenComments.Count > 0)
        {
            formattedComment.ChildrenComments = comment.ChildrenComments
                .Select(Format)
                .ToList();
        }

        return formattedComment;
    }
}