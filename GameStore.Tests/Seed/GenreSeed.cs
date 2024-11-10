using GameStore.Core.Models;

namespace GameStore.Tests.Seed;

public class GenreSeed
{
    public static Genre Action => new()
    {
        Id = Guid.Parse("d55a9838-ddfa-4e90-bc24-463c2ad969f8"),
        Name = "Action",
    };

    public static Genre Shooter => new()
    {
        Id = Guid.Parse("6589b697-fd37-4db8-86a6-9e7b5e7fab36"),
        Name = "Shooter",
        ParentGenreId = Action.Id,
    };

    public static List<Genre> GetGenres()
    {
        return
        [
            Action,
            Shooter,
        ];
    }
}