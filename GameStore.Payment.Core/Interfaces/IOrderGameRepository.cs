using GameStore.Payment.Core.Models;

namespace GameStore.Payment.Core.Interfaces;

public interface IOrderGameRepository
{
    Task<IEnumerable<OrderGame>> GetByOrderIdAsync(Guid orderId);

    Task InsertAsync(OrderGame orderGame);

    void Update(OrderGame orderGame);
}