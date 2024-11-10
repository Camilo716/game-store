namespace GameStore.Core.Interfaces;

public interface IUnitOfWork
{
    public IPlatformRepository PlatformRepository { get; }

    public IGenreRepository GenreRepository { get; }

    public IPublisherRepository PublisherRepository { get; }

    public IGameRepository GameRepository { get; }

    Task SaveChangesAsync();
}