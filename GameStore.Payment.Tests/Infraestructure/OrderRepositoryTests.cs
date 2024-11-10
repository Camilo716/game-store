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
        List<OrderStatus> orderStatuses =
        [
            OrderStatus.Open
        ];

        var orders = await unitOfWork.OrderRepository.GetByStatusAsync(orderStatuses);

        Assert.NotNull(orders);
        Assert.All(orders, o => Assert.Equal(OrderStatus.Open, o.Status));
    }
}