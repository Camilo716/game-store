namespace GameStore.Payment.Core.GameClient;

public class Game
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Key { get; set; }

    public string? Description { get; set; }

    public double Price { get; set; }

    public int UnitsInStock { get; set; }

    public int Discount { get; set; }
}