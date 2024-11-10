using GameStore.Core.Models;

namespace GameStore.Core.Interfaces;

public interface IPlatformRepository
{
    public Task<Platform> GetByIdAsync(Guid id);

    public Task<IEnumerable<Platform>> GetAllAsync();

    public Task DeleteByIdAsync(Guid id);

    public Task InsertAsync(Platform platform);

    public void Update(Platform platform);

    Task<IEnumerable<Platform>> GetByGameKeyAsync(string gameKey);
}