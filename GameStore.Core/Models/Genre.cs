namespace GameStore.Core.Models;

public class Genre
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; }

    public Guid? ParentGenreId { get; set; }

    public List<Game> Games { get; set; } =
    [
    ];
}