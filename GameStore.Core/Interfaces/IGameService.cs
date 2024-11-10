using GameStore.Core.Models;

namespace GameStore.Core.Interfaces;

public interface IGameService
{
    public Task<Game> GetByIdAsync(Guid id);

    public Task<Game> GetByKeyAsync(string key);

    public Task<IEnumerable<Game>> GetByPlatformIdAsync(Guid platformId);

    public Task<IEnumerable<Game>> GetByGenreIdAsync(Guid genreId);

    public Task<IEnumerable<Game>> GetByPublisherIdAsync(Guid publisherId);

    public Task<IEnumerable<Game>> GetAllAsync();

    public Task CreateAsync(Game game);

    public Task DeleteAsync(Guid id);

    public Task UpdateAsync(Game game);
}