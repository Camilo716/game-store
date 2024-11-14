using GameStore.Auth.Infraestructure.Entities;

namespace GameStore.Auth.Infraestructure.Data.Seed;

public class PrivilegeSeed
{
    public static Privilege AddGame => new()
    {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
        Key = "AddGame",
    };

    public static Privilege DeleteGame => new()
    {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
        Key = "DeleteGame",
    };

    public static Privilege ViewGame => new()
    {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
        Key = "ViewGame",
    };

    public static Privilege UpdateGame => new()
    {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
        Key = "UpdateGame",
    };

    public static List<Privilege> GetPrivileges() =>
    [
        AddGame,
        DeleteGame,
        ViewGame,
        UpdateGame,
    ];
}