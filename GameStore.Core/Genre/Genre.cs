namespace GameStore.Core.Genre;

public class Genre
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; }

    public Guid? ParentGenreId { get; set; }

    public List<Game.Game> Games { get; set; } =
    [
    ];
}