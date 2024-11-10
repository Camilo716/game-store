using GameStore.Payment.Core.Models;

namespace GameStore.Payment.Core.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<Order>> GetCartAsync();

    Task<IEnumerable<Order>> GetOrdersAsync();

    Task AddGameToCartAsync(Guid gameId);
}