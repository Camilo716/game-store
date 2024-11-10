using GameStore.Payment.Core.Models;
using GameStore.Payment.Infraestructure.Data;
using GameStore.Payment.Tests.Seed;

namespace GameStore.Payment.Tests.Infraestructure;

public class OrderGameRepositoryTests
{
    [Fact]
    public async Task GetByOrder_GivenValidOrderId_ReturnsOrderGamesInDb()
    {
        using var dbContext = UnitTestHelper.GetUnitTestDbContext();
        UnitOfWork unitOfWork = new(dbContext);
        Guid orderId = OrderSeed.OpenedOrder.Id;

        IEnumerable<OrderGame> orderGames = await unitOfWork.OrderGameRepository.GetByOrderIdAsync(orderId);

        Assert.NotNull(orderGames);
        Assert.All(orderGames, o => Assert.Equal(orderId, o.OrderId));
    }

    [Fact]
    public async Task Insert_GivenValidOrderGame_InsertsOrderGameInDatabase()
    {
        using var dbContext = UnitTestHelper.GetUnitTestDbContext();
        var unitOfWork = new UnitOfWork(dbContext);
        Order order = await dbContext.Orders.FindAsync(OrderSeed.OpenedOrder.Id);

        var validOrderGame = new OrderGame()
        {
            OrderId = order.Id,
            Order = order,
            Price = 5.0,
            Quantity = 1,
            Discount = 0,
        };

        await unitOfWork.OrderGameRepository.InsertAsync(validOrderGame);
        await unitOfWork.SaveChangesAsync();

        Assert.Equal(OrderGameSeed.GetOrderGames().Count + 1, dbContext.OrderGames.Count());
    }

    [Fact]
    public async Task Update_GivenValidOrderGame_UpdatesOrderGameInDatabase()
    {
        using var dbContext = UnitTestHelper.GetUnitTestDbContext();
        var unitOfWork = new UnitOfWork(dbContext);
        var orderGame = await dbContext.OrderGames.FindAsync(
            OrderGameSeed.OrderGame1.OrderId, OrderGameSeed.OrderGame1.ProductId);
        orderGame.Quantity++;

        unitOfWork.OrderGameRepository.Update(orderGame);
        await unitOfWork.SaveChangesAsync();

        var updatedOrderGame = await dbContext.OrderGames.FindAsync(
            OrderGameSeed.OrderGame1.OrderId, OrderGameSeed.OrderGame1.ProductId);
        Assert.Equal(orderGame, updatedOrderGame);
    }

    [Fact]
    public async Task Delete_GivenValidId_DeletesOrderGameInDatabase()
    {
        using var dbContext = UnitTestHelper.GetUnitTestDbContext();
        var unitOfWork = new UnitOfWork(dbContext);
        Guid orderId = OrderGameSeed.OrderGame1.OrderId;
        Guid productId = OrderGameSeed.OrderGame1.ProductId;

        await unitOfWork.OrderGameRepository.DeleteByKeyAsync(orderId, productId);
        await unitOfWork.SaveChangesAsync();

        Assert.Equal(OrderGameSeed.GetOrderGames().Count - 1, dbContext.OrderGames.Count());
    }
}