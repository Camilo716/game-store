using GameStore.Auth.Infraestructure.Entities;

namespace GameStore.Auth.Tests.Seed;

public static class UserSeed
{
    public static User UserManager => new()
    {
        Id = "76e82cd5-3881-4c3d-add7-1fae34d9d0cf",
        UserName = "User Manager",
    };

    public static List<User> GetUsers() =>
    [
        UserManager,
    ];
}