using GameStore.Payment.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Payment.Infraestructure.Data;

public class GameStorePaymentDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}