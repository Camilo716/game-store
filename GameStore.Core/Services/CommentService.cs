using GameStore.Core.Interfaces;
using GameStore.Core.Models;

namespace GameStore.Core.Services;

public class CommentService(
    IUnitOfWork unitOfWork)
    : ICommentService
{
    public Task<IEnumerable<Comment>> GetByGameKeyAsync(string gameKey)
    {
        return unitOfWork.CommentRepository.GetByGameKeyAsync(gameKey);
    }
}