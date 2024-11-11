using GameStore.Payment.Core.Dtos;
using GameStore.Payment.Core.Enums;
using GameStore.Payment.Core.GameClient;
using GameStore.Payment.Core.Interfaces;
using GameStore.Payment.Core.Models;

namespace GameStore.Payment.Core.Services;

public class OrderService(
    IUnitOfWork unitOfWork,
    IGameServiceClient gameServiceClient,
    IDateTimeProvider dateTimeProvider,
    IPaymentService paymentService)
    : IOrderService
{
    private IGameServiceClient GameServiceClient => gameServiceClient;

    private IUnitOfWork UnitOfWork => unitOfWork;

    private IDateTimeProvider DateTimeProvider => dateTimeProvider;

    private IPaymentService PaymentService => paymentService;

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

    public async Task DeleteGameFromCartAsync(string gameKey)
    {
        Order cart = (await GetCartAsync())
            .FirstOrDefault()
            ?? throw new InvalidOperationException($"Open cart not found");

        Game game = await GameServiceClient.GetByKeyAsync(gameKey)
            ?? throw new InvalidOperationException($"Game {gameKey} not found");

        await unitOfWork.OrderGameRepository.DeleteByKeyAsync(cart.Id, game.Id);

        IEnumerable<OrderGame> gamesInOrder = await unitOfWork.OrderGameRepository.GetByOrderIdAsync(cart.Id);

        bool isLastGameInOrder = gamesInOrder.Count() == 1 && gamesInOrder.First().ProductId == game.Id;

        if (isLastGameInOrder)
        {
            await unitOfWork.OrderRepository.DeleteByIdAsync(cart.Id);
        }

        await unitOfWork.SaveChangesAsync();
    }

    public async Task<PaymentResponse> PayOrderAsync(PaymentRequest paymentRequest)
    {
        Order cart = (await GetCartAsync())
            .FirstOrDefault()
            ?? throw new InvalidOperationException($"Open cart not found");

        cart.Status = OrderStatus.Paid;

        UnitOfWork.OrderRepository.Update(cart);
        await UnitOfWork.SaveChangesAsync();

        return await PaymentService.HandlePaymentAsync(paymentRequest, cart);
    }

    public async Task AddGameToCartAsync(string gameKey)
    {
        Order cart = (await GetCartAsync()).FirstOrDefault();
        cart ??= await CreateNewOrderAsync();

        Game game = await GameServiceClient.GetByKeyAsync(gameKey)
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

    public async Task<IEnumerable<OrderGame>> GetOrderDetailsAsync(Guid orderId)
    {
        return await UnitOfWork.OrderGameRepository.GetByOrderIdAsync(orderId);
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