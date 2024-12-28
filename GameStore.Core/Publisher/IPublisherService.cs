namespace GameStore.Core.Publisher;

public interface IPublisherService
{
    public Task<Publisher> GetByIdAsync(Guid id);

    public Task<IEnumerable<Publisher>> GetAllAsync();

    public Task DeleteAsync(Guid id);

    public Task CreateAsync(Publisher publisher);

    public Task UpdateAsync(Publisher publisher);

    public Task<Publisher> GetByGameKeyAsync(string gameKey);

    public Task<IEnumerable<Publisher>> GetByCompanyNameAsync(string companyName);
}