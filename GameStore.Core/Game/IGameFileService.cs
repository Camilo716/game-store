namespace GameStore.Core.Game;

public interface IGameFileService
{
    public Task<GameFile> GetByKeyAsync(string key);
}