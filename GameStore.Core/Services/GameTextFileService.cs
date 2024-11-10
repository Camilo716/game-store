using System.Text.Json;
using GameStore.Core.Dtos;
using GameStore.Core.Interfaces;

namespace GameStore.Core.Services;

public class GameTextFileService(IUnitOfWork unitOfWork)
    : IGameFileService
{
    private IUnitOfWork UnitOfWork => unitOfWork;

    public async Task<GameFile> GetByKeyAsync(string key)
    {
        var game = await UnitOfWork.GameRepository.GetByKeyAsync(key);
        byte[] content = JsonSerializer.SerializeToUtf8Bytes(game);

        return new GameFile()
        {
            FileName = $"_{game.Key}.txt",
            ContentType = "application/octet-stream",
            Content = new MemoryStream(content),
        };
    }
}