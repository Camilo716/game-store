namespace GameStore.Payment.Core.Interfaces;

public interface IUnitOfWork
{
    public IOrderRepository OrderRepository { get; }

    Task SaveChangesAsync();
}