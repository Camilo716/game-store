using System.Text.Json;
using GameStore.Core.Interfaces;
using GameStore.Core.Models;

namespace GameStore.Core.Services;

public class GameService
    (IUnitOfWork unitOfWork) : IGameService
{
    private IUnitOfWork UnitOfWork => unitOfWork;

    public async Task CreateAsync(Game game)
    {
        GenerateGameKeyIfNotProvided(game);

        await UnitOfWork.GameRepository.InsertAsync(game);
        await UnitOfWork.SaveChangesAsync();
    }

    public async Task<IEnumerable<Game>> GetAllAsync()
    {
        var games = await UnitOfWork.GameRepository.GetAllAsync();
        return games;
    }

    public async Task DeleteAsync(Guid id)
    {
        await UnitOfWork.GameRepository.DeleteByIdAsync(id);
        await UnitOfWork.SaveChangesAsync();
    }

    public async Task UpdateAsync(Game game)
    {
        GenerateGameKeyIfNotProvided(game);

        UnitOfWork.GameRepository.Update(game);
        await UnitOfWork.SaveChangesAsync();
    }

    public async Task<Game> GetByIdAsync(Guid id)
    {
        return await UnitOfWork.GameRepository.GetByIdAsync(id);
    }

    public async Task<Game> GetByKeyAsync(string key)
    {
        return await UnitOfWork.GameRepository.GetByKeyAsync(key);
    }

    public async Task<IEnumerable<Game>> GetByPlatformIdAsync(Guid platformId)
    {
        return await UnitOfWork.GameRepository.GetByPlatformIdAsync(platformId);
    }

    public async Task<IEnumerable<Game>> GetByGenreIdAsync(Guid genreId)
    {
        return await UnitOfWork.GameRepository.GetByGenreIdAsync(genreId);
    }

    public async Task<IEnumerable<Game>> GetByPublisherIdAsync(Guid publisherId)
    {
        return await UnitOfWork.GameRepository.GetByPublisherIdAsync(publisherId);
    }

    private static void GenerateGameKeyIfNotProvided(Game game)
    {
        if (!string.IsNullOrWhiteSpace(game.Key))
        {
            return;
        }

        game.Key = JsonNamingPolicy.CamelCase.ConvertName(game.Name).Replace(" ", string.Empty);
    }
}