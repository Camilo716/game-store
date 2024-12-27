using GameStore.Core.Models;

namespace GameStore.Core.Interfaces;

public interface ICommentService
{
    Task<IEnumerable<Comment>> GetByGameKeyAsync(string gameKey);
}