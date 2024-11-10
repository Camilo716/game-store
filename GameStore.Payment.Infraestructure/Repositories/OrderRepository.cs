using GameStore.Payment.Core.Enums;
using GameStore.Payment.Core.Interfaces;
using GameStore.Payment.Core.Models;
using GameStore.Payment.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Payment.Infraestructure.Repositories;

public class OrderRepository(GameStorePaymentDbContext dbContext)
    : BaseRepository<Order>(dbContext),
    IOrderRepository
{
    public async Task<IEnumerable<Order>> GetByStatusAsync(IEnumerable<OrderStatus> status)
    {
        return await DbSet
            .Where(order => status.Contains(order.Status))
            .ToListAsync();
    }
}