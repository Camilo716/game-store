using GameStore.Auth.Infraestructure.Data;
using GameStore.Auth.Tests.Seed;

internal class DbSeeder
{
    internal static void SeedData(GameStoreAuthDbContext context)
    {
        var privileges = PrivilegeSeed.GetPrivileges();

        context.AddRange(privileges);
        context.SaveChanges();
    }
}