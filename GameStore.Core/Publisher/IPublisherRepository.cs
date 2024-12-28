namespace GameStore.Core.Publisher;

public interface IPublisherRepository
{
    public Task<Publisher> GetByIdAsync(Guid id);

    public Task<IEnumerable<Publisher>> GetAllAsync();

    public Task DeleteByIdAsync(Guid id);

    public Task InsertAsync(Publisher publisher);

    public void Update(Publisher publisher);

    Task<Publisher> GetByGameKeyAsync(string gameKey);

    public Task<IEnumerable<Publisher>> GetByCompanyNameAsync(string companyName);
}