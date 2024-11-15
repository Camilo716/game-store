using GameStore.Payment.Core.Enums;
using GameStore.Payment.Core.Models;
using GameStore.Payment.Infraestructure.Data;
using GameStore.Payment.Tests.Seed;

namespace GameStore.Payment.Tests.Infraestructure;

public class OrderRepositoryTests
{
    [Fact]
    public async Task GetById_GivenValidId_ReturnsOrderInDatabase()
    {
        using var dbContext = UnitTestHelper.GetUnitTestDbContext();
        var unitOfWork = new UnitOfWork(dbContext);
        Guid id = OrderSeed.OpenedOrder.Id;

        var order = await unitOfWork.OrderRepository.GetByIdAsync(id);

        Assert.NotNull(order);
        Assert.Equal(id, order.Id);
    }

    [Fact]
    public async Task Insert_GivenValidOrder_InsertsOrderInDatabase()
    {
        using var dbContext = UnitTestHelper.GetUnitTestDbContext();
        var unitOfWork = new UnitOfWork(dbContext);
        var validOrder = new Order()
        {
            Status = OrderStatus.Open,
            Date = OrderSeed.OpenedOrder.Date,
            CustomerId = OrderSeed.OpenedOrder.CustomerId,
        };

        await unitOfWork.OrderRepository.InsertAsync(validOrder);
        await unitOfWork.SaveChangesAsync();

        Assert.Equal(OrderSeed.GetOrders().Count + 1, dbContext.Orders.Count());
    }

    [Fact]
    public async Task GetByStatus_GivenValidStatus_ReturnsCorrectOrdersInDatabase()
    {
        using var dbContext = UnitTestHelper.GetUnitTestDbContext();
        var unitOfWork = new UnitOfWork(dbContext);
        List<OrderStatus> orderStatuses =
        [
            OrderStatus.Open
        ];

        var orders = await unitOfWork.OrderRepository.GetByStatusAsync(orderStatuses);

        Assert.NotNull(orders);
        Assert.All(orders, o => Assert.Equal(OrderStatus.Open, o.Status));
    }

    [Fact]
    public async Task Delete_GivenValidId_DeletesOrderInDatabase()
    {
        using var dbContext = UnitTestHelper.GetUnitTestDbContext();
        var unitOfWork = new UnitOfWork(dbContext);
        Guid id = OrderSeed.GetOrders().First().Id;

        await unitOfWork.OrderRepository.DeleteByIdAsync(id);
        await unitOfWork.SaveChangesAsync();

        Assert.Equal(OrderSeed.GetOrders().Count - 1, dbContext.Orders.Count());
    }

    [Fact]
    public async Task Update_GivenValidOrder_UpdatesOrderInDatabase()
    {
        using var dbContext = UnitTestHelper.GetUnitTestDbContext();
        var unitOfWork = new UnitOfWork(dbContext);
        var order = await dbContext.Orders.FindAsync(OrderSeed.OpenedOrder.Id);
        order.Status = OrderStatus.Paid;

        unitOfWork.OrderRepository.Update(order);
        await unitOfWork.SaveChangesAsync();

        var updatedOrder = await dbContext.Orders.FindAsync(OrderSeed.OpenedOrder.Id);
        Assert.Equal(OrderStatus.Paid, updatedOrder.Status);
    }
}