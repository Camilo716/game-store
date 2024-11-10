using GameStore.Payment.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Payment.Infraestructure.Data;

public class GameStorePaymentDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderGame> OrderGames { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderGame>()
            .HasKey(x => new { x.OrderId, x.ProductId });
    }
}