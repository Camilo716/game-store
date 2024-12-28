using GameStore.Core.Models;

namespace GameStore.Core.Interfaces;

public interface ICommentService
{
    Task CreateAsync(Comment comment, string gameKey);

    Task<IEnumerable<Comment>> GetByGameKeyAsync(string gameKey);
}