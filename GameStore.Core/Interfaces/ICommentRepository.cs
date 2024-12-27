using GameStore.Core.Models;

namespace GameStore.Core.Interfaces;

public interface ICommentRepository
{
    Task<IEnumerable<Comment>> GetByGameKeyAsync(string key);
}