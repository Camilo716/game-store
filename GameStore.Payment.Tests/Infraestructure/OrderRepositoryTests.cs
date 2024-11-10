using GameStore.Payment.Core.Enums;
using GameStore.Payment.Infraestructure.Data;

namespace GameStore.Payment.Tests.Infraestructure;

public class OrderRepositoryTests
{
    [Fact]
    public async Task GetByStatus_GivenValidStatus_ReturnsCorrectOrdersInDatabase()
    {
        using var dbContext = UnitTestHelper.GetUnitTestDbContext();
        var unitOfWork = new UnitOfWork(dbContext);

        var orders = await unitOfWork.OrderRepository.GetByStatusAsync(OrderStatus.Open);

        Assert.NotNull(orders);
        Assert.All(orders, o => Assert.Equal(OrderStatus.Open, o.Status));
    }
}