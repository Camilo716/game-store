using GameStore.Payment.Infraestructure.Data;
using GameStore.Payment.Tests.Seed;

internal class DbSeeder
{
    internal static void SeedData(GameStorePaymentDbContext context)
    {
        var orders = OrderSeed.GetOrders();
        var orderGames = OrderGameSeed.GetOrderGames();

        context.OrderGames.AddRange(orderGames);
        context.Orders.AddRange(orders);

        context.SaveChanges();
    }
}