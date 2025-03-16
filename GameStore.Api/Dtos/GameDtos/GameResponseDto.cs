using GameStore.Core.Game;

namespace GameStore.Api.Dtos.GameDtos;

public class GameResponseDto(Game game)
{
    public Guid Id => Game.Id;

    public string Description => Game.Description;

    public string Key => Game.Key;

    public string Name => Game.Name;

    public double Price => Game.Price;

    public int UnitsInStock => Game.UnitsInStock;

    public double Discount => Game.Discount;

    public Guid PublisherId => Game.PublisherId;

    private Game Game => game;
}