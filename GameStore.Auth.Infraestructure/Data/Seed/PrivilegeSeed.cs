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

    public static Privilege ViewGames => new()
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

    public static Privilege AddRole => new()
    {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000007"),
        Key = nameof(Permissions.AddRole),
    };

    public static Privilege DeleteRole => new()
    {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000009"),
        Key = nameof(Permissions.DeleteRole),
    };

    public static Privilege UpdateRole => new()
    {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000008"),
        Key = nameof(Permissions.UpdateRole),
    };

    public static Privilege ViewUsers => new()
    {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000010"),
        Key = nameof(Permissions.ViewUsers),
    };

    public static Privilege AddUser => new()
    {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000011"),
        Key = nameof(Permissions.AddUser),
    };

    public static Privilege DeleteUser => new()
    {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000012"),
        Key = nameof(Permissions.DeleteUser),
    };

    public static Privilege UpdateUser => new()
    {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000013"),
        Key = nameof(Permissions.UpdateUser),
    };

    public static Privilege ViewGenres => new()
    {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000014"),
        Key = nameof(Permissions.ViewGenres),
    };

    public static Privilege AddGenre => new()
    {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000015"),
        Key = nameof(Permissions.AddGenre),
    };

    public static Privilege DeleteGenre => new()
    {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000016"),
        Key = nameof(Permissions.DeleteGenre),
    };

    public static Privilege UpdateGenre => new()
    {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000017"),
        Key = nameof(Permissions.UpdateGenre),
    };

    public static Privilege ViewPlatforms => new()
    {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000018"),
        Key = nameof(Permissions.ViewPlatforms),
    };

    public static Privilege AddPlatform => new()
    {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000019"),
        Key = nameof(Permissions.AddPlatform),
    };

    public static Privilege DeletePlatform => new()
    {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000020"),
        Key = nameof(Permissions.DeletePlatform),
    };

    public static Privilege UpdatePlatform => new()
    {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000021"),
        Key = nameof(Permissions.UpdatePlatform),
    };

    public static Privilege ViewPublishers => new()
    {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000022"),
        Key = nameof(Permissions.ViewPublishers),
    };

    public static Privilege AddPublisher => new()
    {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000023"),
        Key = nameof(Permissions.AddPublisher),
    };

    public static Privilege DeletePublisher => new()
    {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000024"),
        Key = nameof(Permissions.DeletePublisher),
    };

    public static Privilege UpdatePublisher => new()
    {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000025"),
        Key = nameof(Permissions.UpdatePublisher),
    };

    public static List<Privilege> GetPrivileges() =>
    [
        AddGame,
        DeleteGame,
        ViewGames,
        UpdateGame,

        AddUser,
        DeleteUser,
        ViewUsers,
        UpdateUser,

        ViewRoles,
        AddRole,
        UpdateRole,
        DeleteRole,

        ViewGenres,
        AddGenre,
        UpdateGenre,
        DeleteGenre,

        ViewPlatforms,
        AddPlatform,
        UpdatePlatform,
        DeletePlatform,

        ViewPublishers,
        AddPublisher,
        UpdatePublisher,
        DeletePublisher,
    ];
}