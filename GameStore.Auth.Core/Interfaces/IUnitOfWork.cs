namespace GameStore.Auth.Core.Interfaces;

public interface IUnitOfWork
{
    public IPrivilegeRepository PrivilegeRepository { get; }
}