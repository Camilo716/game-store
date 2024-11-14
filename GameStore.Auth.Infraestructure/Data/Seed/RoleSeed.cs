using GameStore.Auth.Infraestructure.Entities;

namespace GameStore.Auth.Infraestructure.Data.Seed;

public static class RoleSeed
{
    public static Role Admin => new()
    {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000010").ToString(),
        Name = "Admin",
        NormalizedName = "ADMIN",
        Privileges =
        [
        ],
    };

    public static Role Manager => new()
    {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000020").ToString(),
        Name = "Manager",
        NormalizedName = "MANAGER",
        ParentRoleId = Admin.Id,
        Privileges =
        [
        ],
    };

    public static Role Moderator => new()
    {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000030").ToString(),
        Name = "Moderator",
        NormalizedName = "MODERATOR",
        ParentRoleId = Manager.Id,
        Privileges =
        [
        ],
    };

    public static Role User => new()
    {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000040").ToString(),
        Name = "User",
        NormalizedName = "USER",
        ParentRoleId = Moderator.Id,
        Privileges =
        [
        ],
    };

    public static Role Guest => new()
    {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000050").ToString(),
        Name = "Guest",
        NormalizedName = "GUEST",
        ParentRoleId = User.Id,
    };

    public static List<Role> GetRoles() =>
    [
        Admin,
        Manager,
        Moderator,
        User,
        Guest,
    ];
}
