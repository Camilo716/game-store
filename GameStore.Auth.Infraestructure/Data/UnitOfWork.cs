using AutoMapper;
using GameStore.Auth.Core.Privilege;
using GameStore.Auth.Core.Role;
using GameStore.Auth.Core.UnitOfWork;
using GameStore.Auth.Core.User;
using GameStore.Auth.Infraestructure.Repositories;

namespace GameStore.Auth.Infraestructure.Data;

public class UnitOfWork(
    GameStoreAuthDbContext dbContext,
    IMapper mapper) : IUnitOfWork
{
    private IPrivilegeRepository _privilegeRepository;
    private IRoleRepository _roleRepository;
    private IUserRepository _userRepository;

    public IPrivilegeRepository PrivilegeRepository
    {
        get
        {
            _privilegeRepository ??= new PrivilegeRepository(dbContext, mapper);
            return _privilegeRepository;
        }
    }

    public IRoleRepository RoleRepository
    {
        get
        {
            _roleRepository ??= new RoleRepository(dbContext, mapper);
            return _roleRepository;
        }
    }

    public IUserRepository UserRepository
    {
        get
        {
            _userRepository ??= new UserRepository(dbContext);
            return _userRepository;
        }
    }

    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}