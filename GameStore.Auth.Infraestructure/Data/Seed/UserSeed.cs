using GameStore.Auth.Infraestructure.Entities;
using Microsoft.AspNetCore.Identity;

namespace GameStore.Auth.Infraestructure.Data.Seed;

public static class UserSeed
{
    public static User DemoAdmin
    {
        get
        {
            var user = new User
            {
                Id = "00000000-0000-0000-0000-000000000001",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
            };

            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, "admin123");

            return user;
        }
    }

    public static List<User> GetUsers() =>
    [
        DemoAdmin,
    ];
}