namespace GameStore.Auth.Core.Interfaces;

public interface IUnitOfWork
{
    public IPrivilegeRepository PrivilegeRepository { get; }

    public IRoleRepository RoleRepository { get; }

    public Task SaveChangesAsync();
}