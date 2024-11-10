using GameStore.Payment.Core.Enums;
using GameStore.Payment.Core.Models;

namespace GameStore.Payment.Tests.Seed;

public class OrderSeed
{
    public static Order OpenedOrder => new()
    {
        Id = Guid.Parse("1f05a293-8374-4688-a47c-e2594bbbbab4"),
        Date = new DateTime(2024, 11, 10),
        CustomerId = Guid.Parse("ac86ea17-fb69-4414-9a68-444bf154e974"),
        Status = OrderStatus.Open,
    };

    public static List<Order> GetOrders() =>
    [
        OpenedOrder,
    ];
}