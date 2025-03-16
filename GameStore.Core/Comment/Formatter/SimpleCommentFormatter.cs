namespace GameStore.Core.Comment.Formatter;

public class SimpleCommentFormatter : ICommentFormatter
{
    public CommentResponse Format(Comment comment)
    {
        string formattedBody = comment.Deleted
            ? Constants.DeletedCommentText
            : comment.Body;

        var response = new CommentResponse().Map(comment);
        response.FormattedBody = formattedBody;
        return response;
    }
}