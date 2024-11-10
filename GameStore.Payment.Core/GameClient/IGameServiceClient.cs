namespace GameStore.Payment.Core.GameClient;

public interface IGameServiceClient
{
    Task<Game> GetByKeyAsync(string key);
}