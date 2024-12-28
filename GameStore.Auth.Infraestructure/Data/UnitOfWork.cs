using AutoMapper;
using GameStore.Auth.Core.Privilege;
using GameStore.Auth.Core.Role;
using GameStore.Auth.Core.UnitOfWork;
using GameStore.Auth.Infraestructure.Repositories;

namespace GameStore.Auth.Infraestructure.Data;

public class UnitOfWork(
    GameStoreAuthDbContext dbContext,
    IMapper mapper) : IUnitOfWork
{
    private IPrivilegeRepository _privilegeRepository;
    private IRoleRepository _roleRepository;

    public IPrivilegeRepository PrivilegeRepository
    {
        get
        {
            _privilegeRepository ??= new PrivilegeRepository(DbContext, Mapper);
            return _privilegeRepository;
        }
    }

    public IRoleRepository RoleRepository
    {
        get
        {
            _roleRepository ??= new RoleRepository(DbContext, mapper);
            return _roleRepository;
        }
    }

    private GameStoreAuthDbContext DbContext => dbContext;

    private IMapper Mapper => mapper;

    public async Task SaveChangesAsync()
    {
        await DbContext.SaveChangesAsync();
    }
}