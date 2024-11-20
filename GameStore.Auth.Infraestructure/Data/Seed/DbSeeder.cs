using GameStore.Auth.Infraestructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Auth.Infraestructure.Data.Seed;

public static class DbSeeder
{
    private const string PrivilegeRole = "PrivilegeRoles";
    private const string PrivilegesId = "PrivilegesId";
    private const string RolesId = "RolesId";

    public static void Seed(ModelBuilder modelBuilder)
    {
        List<Privilege> privileges = PrivilegeSeed.GetPrivileges();
        List<Role> roles = RoleSeed.GetRoles();

        modelBuilder.Entity<Privilege>().HasData([.. privileges]);
        modelBuilder.Entity<Role>().HasData([.. roles]);
        SeedPrivilegeRoleRelations(modelBuilder);
    }

    private static void SeedPrivilegeRoleRelations(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>()
            .HasMany(r => r.Privileges)
            .WithMany(p => p.Roles)
            .UsingEntity<Dictionary<string, object>>(PrivilegeRole)
            .HasData(
                GetRelationship(PrivilegeSeed.ViewUsers.Id, RoleSeed.Admin.Id),
                GetRelationship(PrivilegeSeed.AddUser.Id, RoleSeed.Admin.Id),
                GetRelationship(PrivilegeSeed.DeleteUser.Id, RoleSeed.Admin.Id),
                GetRelationship(PrivilegeSeed.UpdateUser.Id, RoleSeed.Admin.Id),
                GetRelationship(PrivilegeSeed.ViewRoles.Id, RoleSeed.Admin.Id),
                GetRelationship(PrivilegeSeed.AddRole.Id, RoleSeed.Admin.Id),
                GetRelationship(PrivilegeSeed.DeleteRole.Id, RoleSeed.Admin.Id),
                GetRelationship(PrivilegeSeed.UpdateRole.Id, RoleSeed.Admin.Id),
                GetRelationship(PrivilegeSeed.ViewGames.Id, RoleSeed.Guest.Id),
                GetRelationship(PrivilegeSeed.AddGame.Id, RoleSeed.Manager.Id),
                GetRelationship(PrivilegeSeed.DeleteGame.Id, RoleSeed.Manager.Id),
                GetRelationship(PrivilegeSeed.UpdateGame.Id, RoleSeed.Manager.Id),
                GetRelationship(PrivilegeSeed.ViewGenres.Id, RoleSeed.Manager.Id),
                GetRelationship(PrivilegeSeed.AddGenre.Id, RoleSeed.Manager.Id),
                GetRelationship(PrivilegeSeed.UpdateGenre.Id, RoleSeed.Manager.Id),
                GetRelationship(PrivilegeSeed.DeleteGenre.Id, RoleSeed.Manager.Id));
    }

    private static Dictionary<string, object> GetRelationship(Guid privilegeId, string roleId)
    {
        return new Dictionary<string, object> { { PrivilegesId, privilegeId }, { RolesId, roleId } };
    }
}