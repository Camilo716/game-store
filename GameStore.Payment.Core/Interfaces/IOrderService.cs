using GameStore.Payment.Core.Models;

namespace GameStore.Payment.Core.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<Order>> GetCartAsync();
}