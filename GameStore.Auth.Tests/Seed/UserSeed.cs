using GameStore.Auth.Core.User;
using GameStore.Auth.Infraestructure.Entities;
using Microsoft.AspNetCore.Identity;

namespace GameStore.Auth.Tests.Seed;

public static class UserSeed
{
    public static string TestPassword => "SuperSecuredPassword";

    public static User UserManager
    {
        get
        {
            var user = new User
            {
                Id = "76e82cd5-3881-4c3d-add7-1fae34d9d0cf",
                UserName = "UserManager",
                NormalizedUserName = "USERMANAGER",
            };

            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, TestPassword);

            return user;
        }
    }

    public static UserModel UserManagerModel
        => Infraestructure.Mapper.Create().Map<UserModel>(UserManager);

    public static List<User> GetUsers() =>
    [
        UserManager,
    ];
}