using GameStore.Auth.Infraestructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Auth.Infraestructure.Data;

public class GameStoreAuthDbContext(
    DbContextOptions<GameStoreAuthDbContext> options)
    : IdentityDbContext<IdentityUser>(options)
{
    public DbSet<Privilege> Privileges { get; set; }

    public DbSet<RolePrivilege> RolePrivileges { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Privilege>()
            .HasIndex(p => p.Key)
            .IsUnique();

        builder.Entity<RolePrivilege>()
            .HasKey(rp => new { rp.RoleId, rp.PrivilegeId });

        builder.Entity<RolePrivilege>()
            .HasOne(rp => rp.Role)
            .WithMany()
            .HasForeignKey(rp => rp.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<RolePrivilege>()
            .HasOne(rp => rp.Privilege)
            .WithMany(p => p.RolePrivileges)
            .HasForeignKey(rp => rp.PrivilegeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}