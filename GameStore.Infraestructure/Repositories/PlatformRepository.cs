using GameStore.Core.Interfaces;
using GameStore.Core.Models;
using GameStore.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Infraestructure.Repositories;

public class PlatformRepository(GameStoreDbContext dbContext)
    : BaseRepository<Platform>(dbContext),
    IPlatformRepository
{
    public async Task<IEnumerable<Platform>> GetByGameKeyAsync(string gameKey)
    {
        return await DbSet
            .Include(platform => platform.Games)
            .Where(platform => platform.Games.Select(g => g.Key).Contains(gameKey))
            .ToListAsync();
    }
}