using AutoMapper;
using GameStore.Auth.Core.Interfaces;
using GameStore.Auth.Infraestructure.Repositories;

namespace GameStore.Auth.Infraestructure.Data;

public class UnitOfWork(
    GameStoreAuthDbContext dbContext,
    IMapper mapper) : IUnitOfWork
{
    private IPrivilegeRepository _privilegeRepository;

    public IPrivilegeRepository PrivilegeRepository
    {
        get
        {
            _privilegeRepository ??= new PrivilegeRepository(DbContext, Mapper);
            return _privilegeRepository;
        }
    }

    private GameStoreAuthDbContext DbContext => dbContext;

    private IMapper Mapper => mapper;

    public async Task SaveChangesAsync()
    {
        await DbContext.SaveChangesAsync();
    }
}