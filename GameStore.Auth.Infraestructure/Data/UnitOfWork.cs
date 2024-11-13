using GameStore.Auth.Core.Interfaces;
using GameStore.Auth.Infraestructure.Repositories;

namespace GameStore.Auth.Infraestructure.Data;

public class UnitOfWork(GameStoreAuthDbContext dbContext) : IUnitOfWork
{
    private IPrivilegeRepository _privilegeRepository;

    public IPrivilegeRepository PrivilegeRepository
    {
        get
        {
            _privilegeRepository ??= new PrivilegeRepository(DbContext);
            return _privilegeRepository;
        }
    }

    private GameStoreAuthDbContext DbContext => dbContext;

    public async Task SaveChangesAsync()
    {
        await DbContext.SaveChangesAsync();
    }
}