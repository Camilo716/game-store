using GameStore.Auth.Core.Privilege;
using GameStore.Auth.Core.Role;
using GameStore.Auth.Core.User;

namespace GameStore.Auth.Core.UnitOfWork;

public interface IUnitOfWork
{
    public IPrivilegeRepository PrivilegeRepository { get; }

    public IRoleRepository RoleRepository { get; }

    public IUserRepository UserRepository { get; }

    public Task SaveChangesAsync();
}