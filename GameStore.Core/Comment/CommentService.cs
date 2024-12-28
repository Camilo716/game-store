using GameStore.Core.UnitOfWork;

namespace GameStore.Core.Comment;

public class CommentService(
    IUnitOfWork unitOfWork)
    : ICommentService
{
    public async Task CreateAsync(Comment comment, string gameKey)
    {
        Game.Game game = await unitOfWork.GameRepository.GetByKeyAsync(gameKey);
        comment.GameId = game.Id;
        await unitOfWork.CommentRepository.InsertAsync(comment);
        await unitOfWork.SaveChangesAsync();
    }

    public Task<IEnumerable<Comment>> GetByGameKeyAsync(string gameKey)
    {
        return unitOfWork.CommentRepository.GetByGameKeyAsync(gameKey);
    }
}