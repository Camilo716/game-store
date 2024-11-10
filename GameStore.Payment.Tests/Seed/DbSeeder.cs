using GameStore.Payment.Infraestructure.Data;
using GameStore.Payment.Tests.Seed;

internal class DbSeeder
{
    internal static void SeedData(GameStorePaymentDbContext context)
    {
        var orders = OrderSeed.GetOrders();

        context.Orders.AddRange(orders);

        context.SaveChanges();
    }
}