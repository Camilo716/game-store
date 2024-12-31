using Microsoft.EntityFrameworkCore;

namespace GameStore.Payment.Infraestructure.Data;

public class DatabaseInitializer(GameStorePaymentDbContext dbContext) : IDatabaseInitializer
{
    public void Initialize()
    {
        dbContext.Database.Migrate();
    }
}