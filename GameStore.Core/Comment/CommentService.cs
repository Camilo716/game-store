using GameStore.Core.Comment.Formatter;
using GameStore.Core.UnitOfWork;

namespace GameStore.Core.Comment;

public class CommentService(
    IUnitOfWork unitOfWork,
    ICommentFormatter commentFormatter)
    : ICommentService
{
    public async Task CreateAsync(Comment comment, string gameKey)
    {
        Game.Game game = await unitOfWork.GameRepository.GetByKeyAsync(gameKey);
        comment.GameId = game.Id;
        await unitOfWork.CommentRepository.InsertAsync(comment);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task<IEnumerable<CommentResponse>> GetByGameKeyAsync(string gameKey)
    {
        var comments = await unitOfWork.CommentRepository.GetByGameKeyAsync(gameKey);

        return comments.Select(commentFormatter.Format);
    }
}