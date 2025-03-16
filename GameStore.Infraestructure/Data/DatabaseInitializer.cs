using Microsoft.EntityFrameworkCore;

namespace GameStore.Infraestructure.Data;

public class DatabaseInitializer(GameStoreDbContext dbContext) : IDatabaseInitializer
{
    public void Initialize()
    {
        dbContext.Database.Migrate();
    }
}