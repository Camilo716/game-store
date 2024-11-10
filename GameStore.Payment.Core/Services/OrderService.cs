using GameStore.Payment.Core.Enums;
using GameStore.Payment.Core.Interfaces;
using GameStore.Payment.Core.Models;

namespace GameStore.Payment.Core.Services;

public class OrderService(
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider)
    : IOrderService
{
    private IUnitOfWork UnitOfWork => unitOfWork;

    private IDateTimeProvider DateTimeProvider => dateTimeProvider;

    public async Task<IEnumerable<Order>> GetCartAsync()
    {
        return await UnitOfWork.OrderRepository.GetByStatusAsync([
            OrderStatus.Open
        ]);
    }

    public async Task<IEnumerable<Order>> GetOrdersAsync()
    {
        return await UnitOfWork.OrderRepository.GetByStatusAsync([
            OrderStatus.Paid,
            OrderStatus.Cancelled,
        ]);
    }

    public async Task AddGameToCartAsync(Guid gameId)
    {
        Order cart = (await GetCartAsync()).FirstOrDefault();
        cart ??= await CreateNewOrderAsync();

        OrderGame? existingGameInOrder = (await UnitOfWork.OrderGameRepository
            .GetByOrderIdAsync(cart.Id))
            .FirstOrDefault(orderGame => orderGame.ProductId == gameId);

        if (existingGameInOrder is null)
        {
            await AddOrderGameToOrderAsync(gameId, cart);
        }
        else
        {
            await IncreaseQuantityOfExistingOrderGame(existingGameInOrder);
        }
    }

    private async Task IncreaseQuantityOfExistingOrderGame(OrderGame? orderGame)
    {
        orderGame.Quantity++;
        unitOfWork.OrderGameRepository.Update(orderGame);
        await unitOfWork.SaveChangesAsync();
    }

    private async Task<Order> CreateNewOrderAsync()
    {
        Order order = new()
        {
            CustomerId = Guid.NewGuid(),
            Date = DateTimeProvider.Now(),
            Status = OrderStatus.Open,
        };

        await UnitOfWork.OrderRepository.InsertAsync(order);
        return order;
    }

    private async Task AddOrderGameToOrderAsync(Guid gameId, Order cart)
    {
        OrderGame orderGame = new()
        {
            OrderId = cart.Id,
            ProductId = gameId,
            Quantity = 1,
            Price = 1,
        };

        await UnitOfWork.OrderGameRepository.InsertAsync(orderGame);
        await UnitOfWork.SaveChangesAsync();
    }
}