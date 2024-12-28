using GameStore.Auth.Core.Privilege;
using GameStore.Auth.Core.Role;

namespace GameStore.Auth.Core.UnitOfWork;

public interface IUnitOfWork
{
    public IPrivilegeRepository PrivilegeRepository { get; }

    public IRoleRepository RoleRepository { get; }

    public Task SaveChangesAsync();
}