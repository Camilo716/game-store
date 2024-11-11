using GameStore.Payment.Core.Enums;
using GameStore.Payment.Core.GameClient;
using GameStore.Payment.Core.Interfaces;
using GameStore.Payment.Core.Models;
using GameStore.Payment.Core.Services;
using GameStore.Payment.Tests.Seed;
using Moq;

namespace GameStore.Payment.Tests.Core;

public class OrderServiceTests
{
    [Fact]
    public async Task GetCart_ReturnsOpenedOrders()
    {
        Mock<IUnitOfWork> unitOfWork = GetDummyUnitOfWorkMock();
        OrderService orderService = GetOrderService(unitOfWork);
        List<OrderStatus> orderCartStatuses =
        [
            OrderStatus.Open,
        ];

        IEnumerable<Order> orders = await orderService.GetCartAsync();

        Assert.NotNull(orders);
        unitOfWork.Verify(
            m => m.OrderRepository.GetByStatusAsync(orderCartStatuses),
            Times.Once());
    }

    [Fact]
    public async Task GetOrders_ReturnsPaidAndCancelledOrders()
    {
        Mock<IUnitOfWork> unitOfWork = GetDummyUnitOfWorkMock();
        OrderService orderService = GetOrderService(unitOfWork);
        List<OrderStatus> orderStatuses =
        [
            OrderStatus.Paid,
            OrderStatus.Cancelled,
        ];

        IEnumerable<Order> orders = await orderService.GetOrdersAsync();

        Assert.NotNull(orders);
        unitOfWork.Verify(
            m => m.OrderRepository.GetByStatusAsync(orderStatuses),
            Times.Once());
    }

    [Fact]
    public async Task GetOrderDetails_ReturnsCorrectOrderGames()
    {
        Mock<IUnitOfWork> unitOfWork = GetDummyUnitOfWorkMock();
        OrderService orderService = GetOrderService(unitOfWork);
        Guid orderId = OrderSeed.OpenedOrder.Id;

        IEnumerable<OrderGame> orderGames = await orderService.GetOrderDetailsAsync(orderId);

        Assert.NotNull(orderGames);
        unitOfWork.Verify(
            m => m.OrderGameRepository.GetByOrderIdAsync(orderId),
            Times.Once());
    }

    [Fact]
    public async Task AddGameToCart_WhenOrderExists_AddsOrderGameToOrder()
    {
        // Arrange
        OrderGame createdOrderGame = null;
        Order existingOrder = OrderSeed.OpenedOrder;
        Mock<IUnitOfWork> unitOfWork = new();
        Game game = GetGame();

        unitOfWork.Setup(m => m
            .OrderRepository.GetByStatusAsync(It.IsAny<IEnumerable<OrderStatus>>()))
            .ReturnsAsync([existingOrder]);

        unitOfWork.Setup(m => m
            .OrderGameRepository.InsertAsync(It.IsAny<OrderGame>()))
            .Callback<OrderGame>(og => createdOrderGame = og);

        OrderService orderService = GetOrderService(unitOfWork);

        // Act
        await orderService.AddGameToCartAsync(game.Key);

        // Assert
        Assert.Equal(OrderSeed.OpenedOrder.Id, createdOrderGame?.OrderId);
        Assert.Equal(game.Id, createdOrderGame?.ProductId);
    }

    [Fact]
    public async Task AddGameToCart_WhenOrderDoesNotExists_CreatesOrderBefore()
    {
        // Arrange
        Order createdOrder = null;
        OrderGame createdOrderGame = null;
        Mock<IUnitOfWork> unitOfWork = new();
        Game game = GetGame();

        unitOfWork.Setup(m => m
            .OrderRepository.GetByStatusAsync(It.IsAny<IEnumerable<OrderStatus>>()))
            .ReturnsAsync([]); // Order does not exists

        unitOfWork.Setup(m => m
            .OrderGameRepository.GetByOrderIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync([]);

        unitOfWork.Setup(m => m
            .OrderRepository.InsertAsync(It.IsAny<Order>()))
            .Callback<Order>(o => createdOrder = o);

        unitOfWork.Setup(m => m
            .OrderGameRepository.InsertAsync(It.IsAny<OrderGame>()))
            .Callback<OrderGame>(og => createdOrderGame = og);

        OrderService orderService = GetOrderService(unitOfWork);

        // Act
        await orderService.AddGameToCartAsync(game.Key);

        // Assert
        Assert.NotNull(createdOrder);
        Assert.Equal(createdOrder.Id, createdOrderGame?.OrderId);
        Assert.Equal(game.Id, createdOrderGame?.ProductId);
    }

    [Fact]
    public async Task AddGameToCart_WhenGameAlreadyExistsInOrder_IncreasesQuantity()
    {
        // Arrange
        Mock<IUnitOfWork> unitOfWork = new();
        Order existingOrder = OrderSeed.OpenedOrder;
        OrderGame existingOrderGame = OrderGameSeed.OrderGame1;
        Game game = GetGame();

        unitOfWork.Setup(m => m
            .OrderRepository.GetByStatusAsync(It.IsAny<IEnumerable<OrderStatus>>()))
            .ReturnsAsync([existingOrder]);

        unitOfWork.Setup(m => m
            .OrderGameRepository.GetByOrderIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync([existingOrderGame]); // Order already has the game

        unitOfWork.Setup(m => m
            .OrderGameRepository.Update(It.IsAny<OrderGame>()))
            .Callback<OrderGame>(og => existingOrderGame = og);

        OrderService orderService = GetOrderService(unitOfWork);

        // Act
        await orderService.AddGameToCartAsync(game.Key);

        // Assert
        Assert.Equal(OrderGameSeed.OrderGame1.Quantity + 1, existingOrderGame.Quantity);
    }

    [Fact]
    public async Task DeleteGameFromCart_CorrectlyDeletesGameFromCart()
    {
        // Arrange
        Mock<IUnitOfWork> unitOfWork = new();
        Order existingOrder = OrderSeed.OpenedOrder;
        Game game = GetGame();

        unitOfWork.Setup(m => m
            .OrderRepository.GetByStatusAsync(It.IsAny<IEnumerable<OrderStatus>>()))
            .ReturnsAsync([existingOrder]);

        unitOfWork.Setup(m => m.OrderGameRepository.DeleteByKeyAsync(It.IsAny<Guid>(), It.IsAny<Guid>()));

        OrderService orderService = GetOrderService(unitOfWork);

        // Act
        await orderService.DeleteGameFromCartAsync(game.Key);

        // Assert
        unitOfWork.Verify(m => m.OrderGameRepository.DeleteByKeyAsync(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
    }

    [Fact]
    public async Task DeleteGameFromCart_WhenIsTheLastGameInCart_AlsoDeletesOrder()
    {
        // Arrange
        Mock<IUnitOfWork> unitOfWork = new();
        Order existingOrder = OrderSeed.OpenedOrder;
        OrderGame lastGameInCart = OrderGameSeed.OrderGame1;
        Game game = GetGame();

        unitOfWork.Setup(m => m
            .OrderRepository.GetByStatusAsync(It.IsAny<IEnumerable<OrderStatus>>()))
            .ReturnsAsync([existingOrder]);

        unitOfWork.Setup(m => m
            .OrderGameRepository.GetByOrderIdAsync(existingOrder.Id))
            .ReturnsAsync([lastGameInCart]);

        unitOfWork.Setup(m => m.OrderGameRepository.DeleteByKeyAsync(It.IsAny<Guid>(), It.IsAny<Guid>()));
        unitOfWork.Setup(m => m.OrderRepository.DeleteByIdAsync(It.IsAny<Guid>()));

        OrderService orderService = GetOrderService(unitOfWork);

        // Act
        await orderService.DeleteGameFromCartAsync(game.Key);

        // Assert
        unitOfWork.Verify(m => m.OrderGameRepository.DeleteByKeyAsync(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
        unitOfWork.Verify(m => m.OrderRepository.DeleteByIdAsync(It.IsAny<Guid>()), Times.Once);
    }

    private static Mock<IUnitOfWork> GetDummyUnitOfWorkMock()
    {
        var mock = new Mock<IUnitOfWork>();

        mock.Setup(m => m.OrderRepository
            .GetByStatusAsync(It.IsAny<IEnumerable<OrderStatus>>()))
            .ReturnsAsync(OrderSeed.GetOrders());

        mock.Setup(m => m.OrderGameRepository
            .GetByOrderIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync([OrderGameSeed.OrderGame1]);

        return mock;
    }

    private static Mock<IGameServiceClient> GetDummyGameServiceClientMock()
    {
        var mock = new Mock<IGameServiceClient>();

        mock.Setup(m => m
            .GetByKeyAsync(It.IsAny<string>()))
                .ReturnsAsync(GetGame());

        return mock;
    }

    private static OrderService GetOrderService(Mock<IUnitOfWork> unitOfWork)
    {
        return new OrderService(unitOfWork.Object, GetDummyGameServiceClientMock().Object, new DateTimeProvider());
    }

    private static Game GetGame() => new()
    {
        Id = Guid.Parse("0a2bd33d-030a-4502-9806-c2fdd1b2c4fb"),
        Name = "Days Gone",
        Key = "daysGone",
        Description = "Description",
        Price = 10.0,
        UnitsInStock = 2,
        Discount = 0,
    };
}