using Microsoft.EntityFrameworkCore;

namespace GameStore.Auth.Infraestructure.Data;

public class DatabaseInitializer(GameStoreAuthDbContext dbContext) : IDatabaseInitializer
{
    public void Initialize()
    {
        dbContext.Database.Migrate();
    }
}