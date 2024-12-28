namespace GameStore.Core.Game;

public interface IGameRepository
{
    public Task<Game> GetByIdAsync(Guid id);

    public Task<IEnumerable<Game>> GetAllAsync();

    public Task<Game> GetByKeyAsync(string key);

    Task<IEnumerable<Game>> GetByPlatformIdAsync(Guid platformId);

    Task<IEnumerable<Game>> GetByPublisherIdAsync(Guid publisherId);

    public Task InsertAsync(Game game);

    Task DeleteByIdAsync(Guid id);

    void Update(Game game);

    Task<IEnumerable<Game>> GetByGenreIdAsync(Guid genreId);
}