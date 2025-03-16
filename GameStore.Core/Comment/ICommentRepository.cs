namespace GameStore.Core.Comment;

public interface ICommentRepository
{
    Task<IEnumerable<Comment>> GetByGameKeyAsync(string key);

    Task InsertAsync(Comment comment);

    Task SoftDeleteAsync(Guid id);
}