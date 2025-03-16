using GameStore.Core.Publisher;
using GameStore.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Infraestructure.Repositories;

public class PublisherRepository(GameStoreDbContext dbContext)
    : BaseRepository<Publisher>(dbContext),
    IPublisherRepository
{
    public async Task<IEnumerable<Publisher>> GetByCompanyNameAsync(string companyName)
    {
        return await DbSet
            .Where(publisher => publisher.CompanyName == companyName)
            .ToListAsync();
    }

    public async Task<Publisher> GetByGameKeyAsync(string gameKey)
    {
        return await DbSet
            .Include(publisher => publisher.Games)
            .FirstOrDefaultAsync(publisher => publisher.Games.Select(g => g.Key).Contains(gameKey));
    }
}