using GameStore.Auth.Infraestructure.Data;
using GameStore.Auth.Infraestructure.Entities;
using GameStore.Auth.Tests.Seed;

internal class DbSeeder
{
    internal static void SeedData(GameStoreAuthDbContext context)
    {
        var roles = RoleSeed.GetRoles();
        var privileges = PrivilegeSeed.GetPrivileges();
        var users = UserSeed.GetUsers();

        AttachPrivilegesToRoles(privileges, roles);

        context.Privileges.AddRange(privileges);
        context.Roles.AddRange(roles);
        context.Users.AddRange(users);

        context.SaveChanges();
    }

    private static void AttachPrivilegesToRoles(List<Privilege> privileges, List<Role> roles)
    {
        roles.ForEach(role =>
        {
            var privilegesOfRole = privileges.
                Where(p => role.Privileges.Select(p => p.Id).ToList().Contains(p.Id));

            role.Privileges = privilegesOfRole.ToList();
        });
    }
}