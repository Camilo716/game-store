using System.Text.Json;
using GameStore.Core.UnitOfWork;
using GameModel = GameStore.Core.Game.Game;

namespace GameStore.Core.Game;

public class GameService
    (IUnitOfWork unitOfWork) : IGameService
{
    private IUnitOfWork UnitOfWork => unitOfWork;

    public async Task CreateAsync(GameModel game)
    {
        GenerateGameKeyIfNotProvided(game);

        await UnitOfWork.GameRepository.InsertAsync(game);
        await UnitOfWork.SaveChangesAsync();
    }

    public async Task<IEnumerable<GameModel>> GetAllAsync()
    {
        var games = await UnitOfWork.GameRepository.GetAllAsync();
        return games;
    }

    public async Task DeleteAsync(Guid id)
    {
        await UnitOfWork.GameRepository.DeleteByIdAsync(id);
        await UnitOfWork.SaveChangesAsync();
    }

    public async Task UpdateAsync(GameModel game)
    {
        GenerateGameKeyIfNotProvided(game);

        UnitOfWork.GameRepository.Update(game);
        await UnitOfWork.SaveChangesAsync();
    }

    public async Task<GameModel> GetByIdAsync(Guid id)
    {
        return await UnitOfWork.GameRepository.GetByIdAsync(id);
    }

    public async Task<GameModel> GetByKeyAsync(string key)
    {
        return await UnitOfWork.GameRepository.GetByKeyAsync(key);
    }

    public async Task<IEnumerable<GameModel>> GetByPlatformIdAsync(Guid platformId)
    {
        return await UnitOfWork.GameRepository.GetByPlatformIdAsync(platformId);
    }

    public async Task<IEnumerable<GameModel>> GetByGenreIdAsync(Guid genreId)
    {
        return await UnitOfWork.GameRepository.GetByGenreIdAsync(genreId);
    }

    public async Task<IEnumerable<GameModel>> GetByPublisherIdAsync(Guid publisherId)
    {
        return await UnitOfWork.GameRepository.GetByPublisherIdAsync(publisherId);
    }

    private static void GenerateGameKeyIfNotProvided(GameModel game)
    {
        if (!string.IsNullOrWhiteSpace(game.Key))
        {
            return;
        }

        game.Key = JsonNamingPolicy.CamelCase.ConvertName(game.Name).Replace(" ", string.Empty);
    }
}