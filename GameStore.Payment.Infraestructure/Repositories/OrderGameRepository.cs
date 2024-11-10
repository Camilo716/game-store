using GameStore.Payment.Core.Interfaces;
using GameStore.Payment.Core.Models;
using GameStore.Payment.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Payment.Infraestructure.Repositories;

public class OrderGameRepository(GameStorePaymentDbContext dbContext)
    : BaseRepository<OrderGame>(dbContext),
    IOrderGameRepository
{
    public async Task<IEnumerable<OrderGame>> GetByOrderIdAsync(Guid orderId)
    {
        return await DbSet
            .Where(og => og.OrderId == orderId)
            .ToListAsync();
    }
}