using GameStore.Core.Dtos;

namespace GameStore.Core.Interfaces;

public interface IGameFileService
{
    public Task<GameFile> GetByKeyAsync(string key);
}