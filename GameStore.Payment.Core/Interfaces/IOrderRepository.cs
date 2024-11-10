using GameStore.Payment.Core.Enums;
using GameStore.Payment.Core.Models;

namespace GameStore.Payment.Core.Interfaces;

public interface IOrderRepository
{
    Task<Order> GetByIdAsync(Guid id);

    Task<IEnumerable<Order>> GetByStatusAsync(IEnumerable<OrderStatus> status);

    Task InsertAsync(Order order);

    Task DeleteByIdAsync(Guid id);
}