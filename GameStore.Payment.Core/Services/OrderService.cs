using GameStore.Payment.Core.Enums;
using GameStore.Payment.Core.Interfaces;
using GameStore.Payment.Core.Models;

namespace GameStore.Payment.Core.Services;

public class OrderService(IUnitOfWork unitOfWork)
    : IOrderService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<IEnumerable<Order>> GetCartAsync()
    {
        return await _unitOfWork.OrderRepository.GetByStatusAsync(OrderStatus.Open);
    }
}