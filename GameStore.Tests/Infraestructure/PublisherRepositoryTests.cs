using GameStore.Core.Publisher;
using GameStore.Infraestructure.Data;
using GameStore.Infraestructure.Repositories;
using GameStore.Tests.Seed;

namespace GameStore.Tests.Infraestructure;

public class PublisherRepositoryTests
{
    [Fact]
    public async Task GetById_GivenValidId_ReturnsPublisherInDatabase()
    {
        using var dbContext = UnitTestHelper.GetUnitTestDbContext();
        var publisherRepository = new PublisherRepository(dbContext);
        Guid id = PublisherSeed.GetPublishers().First().Id;

        var publisher = await publisherRepository.GetByIdAsync(id);

        Assert.NotNull(publisher);
        Assert.Equal(id, publisher.Id);
    }

    [Fact]
    public async Task GetAll_ReturnsPublishersInDatabase()
    {
        using var dbContext = UnitTestHelper.GetUnitTestDbContext();
        var publisherRepository = new PublisherRepository(dbContext);

        var publisher = await publisherRepository.GetAllAsync();

        Assert.NotNull(publisher);
        Assert.Equal(PublisherSeed.GetPublishers().Count, publisher.Count());
    }

    [Fact]
    public async Task Delete_GivenValidId_DeletesPublisherInDatabase()
    {
        using var dbContext = UnitTestHelper.GetUnitTestDbContext();
        var unitOfWork = new UnitOfWork(dbContext);
        Guid id = PublisherSeed.GetPublishers().First().Id;

        await unitOfWork.PublisherRepository.DeleteByIdAsync(id);
        await unitOfWork.SaveChangesAsync();

        Assert.Equal(PublisherSeed.GetPublishers().Count - 1, dbContext.Publishers.Count());
    }

    [Fact]
    public async Task Insert_GivenValidPublisher_InsertsPublisherInDatabase()
    {
        using var dbContext = UnitTestHelper.GetUnitTestDbContext();
        var unitOfWork = new UnitOfWork(dbContext);
        var validPublisher = new Publisher() { CompanyName = "Activision 2.0" };

        await unitOfWork.PublisherRepository.InsertAsync(validPublisher);
        await unitOfWork.SaveChangesAsync();

        Assert.Equal(PublisherSeed.GetPublishers().Count + 1, dbContext.Publishers.Count());
    }

    [Fact]
    public async Task Update_GivenValidPublisher_UpdatesPublisherInDatabase()
    {
        using var dbContext = UnitTestHelper.GetUnitTestDbContext();
        var unitOfWork = new UnitOfWork(dbContext);
        var publisher = await dbContext.Publishers.FindAsync(PublisherSeed.Activision.Id);
        publisher.CompanyName = "Activision 2.0";

        unitOfWork.PublisherRepository.Update(publisher);
        await unitOfWork.SaveChangesAsync();

        var updatedPublisher = await dbContext.Publishers.FindAsync(publisher.Id);
        Assert.Equal(publisher, updatedPublisher);
    }

    [Fact]
    public async Task GetByGameKey_GivenValidKey_ReturnsPublishersInDatabase()
    {
        using var dbContext = UnitTestHelper.GetUnitTestDbContext();
        var unitOfWork = new UnitOfWork(dbContext);
        string gameKey = GameSeed.GearsOfWar.Key;

        var publisher = await unitOfWork.PublisherRepository.GetByGameKeyAsync(gameKey);

        Assert.NotNull(publisher);
        Assert.Contains(gameKey, publisher.Games.Select(game => game.Key));
    }

    [Fact]
    public async Task GetByCompanyName_GivenValidCompanyName_ReturnsPublishersInDatabase()
    {
        using var dbContext = UnitTestHelper.GetUnitTestDbContext();
        var publisherRepository = new PublisherRepository(dbContext);
        string companyName = PublisherSeed.Activision.CompanyName;

        var publishers = await publisherRepository.GetByCompanyNameAsync(companyName);

        Assert.NotNull(publishers);
        Assert.All(publishers, p => Assert.Equal(companyName, p.CompanyName));
    }
}