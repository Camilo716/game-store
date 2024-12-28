namespace GameStore.Core.Comment;

public interface ICommentService
{
    Task CreateAsync(Comment comment, string gameKey);

    Task<IEnumerable<Comment>> GetByGameKeyAsync(string gameKey);
}