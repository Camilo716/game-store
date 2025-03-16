using GameStore.Core.UnitOfWork;

namespace GameStore.Core.Publisher;

public class PublisherService(IUnitOfWork unitOfWork) : IPublisherService
{
    private IUnitOfWork UnitOfWork => unitOfWork;

    public async Task<IEnumerable<Publisher>> GetAllAsync()
    {
        return await UnitOfWork.PublisherRepository.GetAllAsync();
    }

    public async Task<Publisher> GetByIdAsync(Guid id)
    {
        return await UnitOfWork.PublisherRepository.GetByIdAsync(id);
    }

    public async Task DeleteAsync(Guid id)
    {
        await UnitOfWork.PublisherRepository.DeleteByIdAsync(id);
        await UnitOfWork.SaveChangesAsync();
    }

    public async Task CreateAsync(Publisher publisher)
    {
        await UnitOfWork.PublisherRepository.InsertAsync(publisher);
        await UnitOfWork.SaveChangesAsync();
    }

    public async Task UpdateAsync(Publisher publisher)
    {
        UnitOfWork.PublisherRepository.Update(publisher);
        await UnitOfWork.SaveChangesAsync();
    }

    public async Task<Publisher> GetByGameKeyAsync(string gameKey)
    {
        return await UnitOfWork.PublisherRepository.GetByGameKeyAsync(gameKey);
    }

    public async Task<IEnumerable<Publisher>> GetByCompanyNameAsync(string companyName)
    {
        return await UnitOfWork.PublisherRepository.GetByCompanyNameAsync(companyName);
    }
}