using GameStore.Core.Interfaces;
using GameStore.Core.Models;
using GameStore.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Infraestructure.Repositories;

public class CommentRepository(GameStoreDbContext dbContext) : ICommentRepository
{
    public async Task<IEnumerable<Comment>> GetByGameKeyAsync(string key)
    {
        return await dbContext.Comments
            .Include(c => c.Game)
            .Where(c => c.Game.Key == key)
            .ToListAsync();
    }
}