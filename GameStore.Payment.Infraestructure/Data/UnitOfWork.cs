using GameStore.Payment.Core.Interfaces;
using GameStore.Payment.Infraestructure.Repositories;

namespace GameStore.Payment.Infraestructure.Data;

public class UnitOfWork(GameStorePaymentDbContext dbContext) : IUnitOfWork
{
    private IOrderRepository _orderRepository;

    private IOrderGameRepository _orderGameRepository;

    public IOrderRepository OrderRepository
    {
        get
        {
            _orderRepository ??= new OrderRepository(DbContext);
            return _orderRepository;
        }
    }

    public IOrderGameRepository OrderGameRepository
    {
        get
        {
            _orderGameRepository ??= new OrderGameRepository(DbContext);
            return _orderGameRepository;
        }
    }

    private GameStorePaymentDbContext DbContext => dbContext;

    public async Task SaveChangesAsync()
    {
        await DbContext.SaveChangesAsync();
    }
}