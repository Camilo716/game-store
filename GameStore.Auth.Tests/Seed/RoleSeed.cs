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
            PrivilegeSeed.DeleteGame,
        ],
    };

    public static Role Manager => new()
    {
        Id = "00000000-0000-0000-0000-000000000001",
        Name = "Manager",
        NormalizedName = "MANAGER",
        ParentRoleId = Admin.Id,
        Privileges =
        [
            PrivilegeSeed.AddGame
        ],
    };

    public static Role Guest => new()
    {
        Id = "00000000-0000-0000-0000-0000000000R3",
        Name = "Guest",
        NormalizedName = "GUEST",
        ParentRoleId = Manager.Id,
        Privileges =
        [
            PrivilegeSeed.ViewGame
        ],
    };

    public static RoleModel AdminModel => Infraestructure.Mapper.Create().Map<RoleModel>(Admin);

    public static List<Role> GetRoles() =>
    [
        Admin,
        Manager,
        Guest
    ];
}