namespace GameStore.Api.Dtos.GameDtos;

public class GamePutRequest
{
    public SimpleGameWithIdDto Game { get; set; }

    public List<Guid> Genres { get; set; } =
    [
    ];

    public List<Guid> Platforms { get; set; } =
    [
    ];

    public Guid? Publisher { get; set; }

    public bool IsValid()
    {
        return Game is not null
            && Game.Id != Guid.Empty
            && !string.IsNullOrWhiteSpace(Game.Name)
            && Game.Price > 0
            && Publisher is not null && Publisher != Guid.Empty;
    }
}