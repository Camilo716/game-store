using GameStore.Payment.Core.Enums;
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
        var orderService = new OrderService(unitOfWork.Object);
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
        var orderService = new OrderService(unitOfWork.Object);
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

    private static Mock<IUnitOfWork> GetDummyUnitOfWorkMock()
    {
        var mock = new Mock<IUnitOfWork>();

        mock.Setup(m => m.OrderRepository
            .GetByStatusAsync(It.IsAny<IEnumerable<OrderStatus>>()))
            .ReturnsAsync(OrderSeed.GetOrders());

        return mock;
    }
}