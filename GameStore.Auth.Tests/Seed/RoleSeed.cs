using GameStore.Auth.Core.Models;
using GameStore.Auth.Infraestructure.Entities;

namespace GameStore.Auth.Tests.Seed;

public static class RoleSeed
{
    public static Role Admin => new()
    {
        Id = "51366600-d6df-4325-b438-999680fecc69",
        Name = "Admin",
        NormalizedName = "ADMIN",
        Privileges =
        [
            PrivilegeSeed.AddGame,
            PrivilegeSeed.ViewGame
        ],
    };

    public static RoleModel AdminModel => Infraestructure.Mapper.Create().Map<RoleModel>(Admin);

    public static List<Role> GetRoles() =>
    [
        Admin,
    ];
}