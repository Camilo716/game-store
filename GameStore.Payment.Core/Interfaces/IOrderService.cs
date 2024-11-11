using GameStore.Payment.Core.Dtos;
using GameStore.Payment.Core.Models;

namespace GameStore.Payment.Core.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<Order>> GetCartAsync();

    Task<IEnumerable<Order>> GetOrdersAsync();

    Task AddGameToCartAsync(string gameKey);

    Task DeleteGameFromCartAsync(string gameKey);

    Task<IEnumerable<OrderGame>> GetOrderDetailsAsync(Guid orderId);

    Task<PaymentResponse> PayOrderAsync(PaymentRequest paymentRequest);
}