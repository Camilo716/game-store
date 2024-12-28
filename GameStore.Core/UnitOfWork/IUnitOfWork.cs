using GameStore.Core.Comment;
using GameStore.Core.Game;
using GameStore.Core.Genre;
using GameStore.Core.Platform;
using GameStore.Core.Publisher;

namespace GameStore.Core.UnitOfWork;

public interface IUnitOfWork
{
    public IPlatformRepository PlatformRepository { get; }

    public IGenreRepository GenreRepository { get; }

    public IPublisherRepository PublisherRepository { get; }

    public IGameRepository GameRepository { get; }

    public ICommentRepository CommentRepository { get; }

    Task SaveChangesAsync();
}