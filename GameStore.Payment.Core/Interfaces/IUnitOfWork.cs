namespace GameStore.Payment.Core.Interfaces;

public interface IUnitOfWork
{
    public IOrderRepository OrderRepository { get; }

    public IOrderGameRepository OrderGameRepository { get; }

    Task SaveChangesAsync();
}