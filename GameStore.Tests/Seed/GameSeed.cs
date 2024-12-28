using GameStore.Core.Game;

namespace GameStore.Tests.Seed;

public static class GameSeed
{
    public static Game GearsOfWar => new()
    {
        Id = Guid.Parse("0a2bd33d-030a-4502-9806-c2fdd1b2c4fb"),
        Name = "Gears of War",
        Key = "GearsOfWar",
        Description = "Description",
        Price = 10.0,
        UnitsInStock = 4,
        Discount = 10,
        Genres =
        [
            GenreSeed.Action,
            GenreSeed.Shooter,
        ],
        Platforms =
        [
            PlatformSeed.Console,
        ],
        PublisherId = PublisherSeed.Activision.Id,
    };

    public static List<Game> GetGames()
    {
        return
        [
            GearsOfWar,
        ];
    }
}