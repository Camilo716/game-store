using GameStore.Core.Models;

namespace GameStore.Core.Interfaces;

public interface IGenreRepository
{
    public Task<Genre> GetByIdAsync(Guid id);

    public Task<IEnumerable<Genre>> GetAllAsync();

    public Task<IEnumerable<Genre>> GetByParentIdAsync(Guid parentId);

    public Task DeleteByIdAsync(Guid id);

    public Task InsertAsync(Genre genre);

    public void Update(Genre genre);

    Task<IEnumerable<Genre>> GetByGameKeyAsync(string gameKey);
}