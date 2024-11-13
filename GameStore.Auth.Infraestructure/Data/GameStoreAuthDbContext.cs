using GameStore.Auth.Infraestructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Auth.Infraestructure.Data;

public class GameStoreAuthDbContext(
    DbContextOptions<GameStoreAuthDbContext> options)
    : IdentityDbContext<User, Role, string>(options)
{
    public DbSet<Privilege> Privileges { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Privilege>()
            .HasIndex(p => p.Key)
            .IsUnique();
    }
}