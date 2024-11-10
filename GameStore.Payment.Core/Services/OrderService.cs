using GameStore.Payment.Core.Enums;
using GameStore.Payment.Core.GameClient;
using GameStore.Payment.Core.Interfaces;
using GameStore.Payment.Core.Models;

namespace GameStore.Payment.Core.Services;

public class OrderService(
    IUnitOfWork unitOfWork,
    IGameServiceClient gameServiceClient,
    IDateTimeProvider dateTimeProvider)
    : IOrderService
{
    private IGameServiceClient GameServiceClient => gameServiceClient;

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

    public async Task AddGameToCartAsync(string gameKey)
    {
        Order cart = (await GetCartAsync()).FirstOrDefault();
        cart ??= await CreateNewOrderAsync();

        Game game = await GameServiceClient.GetByKeyAsync("key")
            ?? throw new InvalidOperationException($"Game {gameKey} not found");

        OrderGame? existingGameInOrder = (await UnitOfWork.OrderGameRepository
            .GetByOrderIdAsync(cart.Id))
            .FirstOrDefault(orderGame => orderGame.ProductId == game.Id);

        if (existingGameInOrder is null)
        {
            await AddOrderGameToOrderAsync(game, cart);
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

    private async Task AddOrderGameToOrderAsync(Game game, Order cart)
    {
        const int quantity = 1;
        OrderGame orderGame = new()
        {
            OrderId = cart.Id,
            ProductId = game.Id,
            Quantity = quantity,
            Price = game.Price * quantity,
        };

        await UnitOfWork.OrderGameRepository.InsertAsync(orderGame);
        await UnitOfWork.SaveChangesAsync();
    }
}