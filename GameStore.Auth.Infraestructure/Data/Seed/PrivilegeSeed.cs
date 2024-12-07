using GameStore.Auth.Core.Enums;
using GameStore.Auth.Infraestructure.Entities;

namespace GameStore.Auth.Infraestructure.Data.Seed;

public class PrivilegeSeed
{
    public static Privilege AddGame => new()
    {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
        Key = nameof(Permissions.AddGame),
    };

    public static Privilege DeleteGame => new()
    {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
        Key = nameof(Permissions.DeleteGame),
    };

    public static Privilege ViewGame => new()
    {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
        Key = nameof(Permissions.ViewGame),
    };

    public static Privilege UpdateGame => new()
    {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
        Key = nameof(Permissions.UpdateGame),
    };

    public static Privilege ViewRoles => new()
    {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
        Key = nameof(Permissions.ViewRoles),
    };

    public static List<Privilege> GetPrivileges() =>
    [
        AddGame,
        DeleteGame,
        ViewGame,
        UpdateGame,
        ViewRoles,
    ];
}