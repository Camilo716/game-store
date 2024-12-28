namespace GameStore.Core.Comment;

public interface ICommentService
{
    Task CreateAsync(Comment comment, string gameKey);

    Task<IEnumerable<CommentResponse>> GetByGameKeyAsync(string gameKey);
}