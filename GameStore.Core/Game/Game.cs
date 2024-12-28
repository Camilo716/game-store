namespace GameStore.Core.Game;

public class Game
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; }

    public string Key { get; set; }

    public string? Description { get; set; }

    public double Price { get; set; }

    public int UnitsInStock { get; set; }

    public int Discount { get; set; }

    public Guid PublisherId { get; set; }

    public Publisher.Publisher Publisher { get; set; }

    public List<Genre.Genre> Genres { get; set; } =
    [
    ];

    public List<Platform.Platform> Platforms { get; set; } =
    [
    ];
}