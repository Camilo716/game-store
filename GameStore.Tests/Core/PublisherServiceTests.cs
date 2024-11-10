using GameStore.Core.Interfaces;
using GameStore.Core.Models;
using GameStore.Core.Services;
using GameStore.Tests.Seed;
using Moq;

namespace GameStore.Tests.Core;

public class PublisherServiceTests
{
    [Fact]
    public async Task GetById_GivenValidId_ReturnsPublisherModel()
    {
        Mock<IUnitOfWork> unitOfWork = GetDummyUnitOfWorkMock();
        var publisherService = new PublisherService(unitOfWork.Object);
        Guid id = PublisherSeed.Activision.Id;

        var publisher = await publisherService.GetByIdAsync(id);

        Assert.NotNull(publisher);
        Assert.Equal(publisher.Id, id);
    }

    [Fact]
    public async Task GetByCompanyName_GivenValidCompanyName_ReturnsPublisherModel()
    {
        Mock<IUnitOfWork> unitOfWork = GetDummyUnitOfWorkMock();
        var publisherService = new PublisherService(unitOfWork.Object);
        string companyName = PublisherSeed.Activision.CompanyName;

        IEnumerable<Publisher> publishers = await publisherService.GetByCompanyNameAsync(companyName);

        Assert.NotNull(publishers);
        Assert.All(publishers, p => Assert.Equal(companyName, p.CompanyName));
    }

    [Fact]
    public async Task GetAll_ReturnsPublishersModels()
    {
        Mock<IUnitOfWork> unitOfWork = GetDummyUnitOfWorkMock();
        var publisherService = new PublisherService(unitOfWork.Object);

        var publishers = await publisherService.GetAllAsync();

        Assert.NotNull(publishers);
        Assert.Equal(PublisherSeed.GetPublishers().Count, publishers.Count());
    }

    [Fact]
    public async Task Delete_GivenValidId_DeletesPublisher()
    {
        Mock<IUnitOfWork> unitOfWork = GetDummyUnitOfWorkMock();
        Guid id = PublisherSeed.GetPublishers().First().Id;
        var publisherService = new PublisherService(unitOfWork.Object);

        await publisherService.DeleteAsync(id);

        unitOfWork.Verify(m => m.PublisherRepository.DeleteByIdAsync(id), Times.Once());
        unitOfWork.Verify(m => m.SaveChangesAsync(), Times.Once());
    }

    [Fact]
    public async Task Create_GivenValidPublisher_CreatesPublisher()
    {
        Mock<IUnitOfWork> unitOfWork = GetDummyUnitOfWorkMock();
        var validPublisher = new Publisher() { CompanyName = "Activision 2.0" };
        var publisherService = new PublisherService(unitOfWork.Object);

        await publisherService.CreateAsync(validPublisher);

        unitOfWork.Verify(
            m => m.PublisherRepository.InsertAsync(It.Is<Publisher>(p => p.CompanyName == validPublisher.CompanyName)),
            Times.Once());
    }

    [Fact]
    public async Task Update_GivenValidPublisher_UpdatesPublisher()
    {
        Mock<IUnitOfWork> unitOfWork = GetDummyUnitOfWorkMock();
        var publisher = new Publisher() { CompanyName = "Activision 2.0" };
        var publisherService = new PublisherService(unitOfWork.Object);

        await publisherService.UpdateAsync(publisher);

        unitOfWork.Verify(
            m => m.PublisherRepository.Update(It.Is<Publisher>(p => p.CompanyName == publisher.CompanyName)),
            Times.Once());
    }

    [Fact]
    public async Task GetByGameKey_GivenValidKey_ReturnsPublishers()
    {
        Mock<IUnitOfWork> unitOfWork = GetDummyUnitOfWorkMock();
        var publisherService = new PublisherService(unitOfWork.Object);
        const string gameKey = "GearsOfWar";

        var publishers = await publisherService.GetByGameKeyAsync(gameKey);

        Assert.NotNull(publishers);
    }

    private static Mock<IUnitOfWork> GetDummyUnitOfWorkMock()
    {
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        mockUnitOfWork.Setup(m => m.PublisherRepository.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(PublisherSeed.Activision);

        mockUnitOfWork.Setup(m => m.PublisherRepository.GetAllAsync())
            .ReturnsAsync(PublisherSeed.GetPublishers());

        mockUnitOfWork.Setup(m => m.PublisherRepository.GetByGameKeyAsync(It.IsAny<string>()))
            .ReturnsAsync(PublisherSeed.Activision);

        mockUnitOfWork.Setup(m => m.PublisherRepository.DeleteByIdAsync(It.IsAny<Guid>()));

        mockUnitOfWork.Setup(m => m.PublisherRepository.InsertAsync(It.IsAny<Publisher>()));

        mockUnitOfWork.Setup(m => m.PublisherRepository.Update(It.IsAny<Publisher>()));

        return mockUnitOfWork;
    }
}