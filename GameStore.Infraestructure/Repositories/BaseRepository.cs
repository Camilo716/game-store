using GameStore.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Infraestructure.Repositories;

public class BaseRepository<TEntity>
    where TEntity : class
{
    public BaseRepository(GameStoreDbContext dbContext)
    {
        DbContext = dbContext;
        DbSet = DbContext.Set<TEntity>();
    }

    protected DbSet<TEntity> DbSet { get; private set; }

    protected GameStoreDbContext DbContext { get; private set; }

    public async Task<TEntity> GetByIdAsync(Guid id)
    {
        var dbRecord = await DbSet.FindAsync(id);
        return dbRecord;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await DbSet.ToListAsync();
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var dbRecord = await GetByIdAsync(id);
        DbContext.Remove(dbRecord);
    }

    public virtual async Task InsertAsync(TEntity entity)
    {
        await DbSet.AddAsync(entity);
    }

    public virtual void Update(TEntity entity)
    {
        var entry = DbContext.Entry(entity);
        entry.State = EntityState.Modified;
    }
}