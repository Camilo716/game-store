using GameStore.Payment.Core.Models;

namespace GameStore.Payment.Tests.Seed;

public class OrderGameSeed
{
    public static OrderGame OrderGame1 => new()
    {
        OrderId = OrderSeed.OpenedOrder.Id,
        ProductId = Guid.Parse("0a2bd33d-030a-4502-9806-c2fdd1b2c4fb"),
        Price = 10.0,
        Quantity = 10,
        Discount = 0,
    };

    public static List<OrderGame> GetOrderGames() =>
    [
        OrderGame1,
    ];
}