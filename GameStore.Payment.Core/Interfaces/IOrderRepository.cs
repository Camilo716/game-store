using GameStore.Payment.Core.Enums;
using GameStore.Payment.Core.Models;

namespace GameStore.Payment.Core.Interfaces;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetByStatusAsync(OrderStatus status);
}