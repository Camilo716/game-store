using GameStore.Core.Interfaces;
using GameStore.Core.Models;
using GameStore.Core.Services;
using GameStore.Tests.Seed;
using Moq;

namespace GameStore.Tests.Core;

public class GenreServiceTest
{
    [Fact]
    public async Task GetById_GivenValidId_ReturnsGenreModel()
    {
        Mock<IUnitOfWork> unitOfWork = GetDummyUnitOfWorkMock();
        var genreService = new GenreService(unitOfWork.Object);
        Guid id = GenreSeed.Action.Id;

        var genre = await genreService.GetByIdAsync(id);

        Assert.NotNull(genre);
        Assert.Equal(genre.Id, id);
    }

    [Fact]
    public async Task GetAll_ReturnsGenreModels()
    {
        Mock<IUnitOfWork> unitOfWork = GetDummyUnitOfWorkMock();
        var genreService = new GenreService(unitOfWork.Object);

        var genres = await genreService.GetAllAsync();

        Assert.NotNull(genres);
        Assert.Equal(GenreSeed.GetGenres().Count, genres.Count());
    }

    [Fact]
    public async Task GetByParentId_GivenValidParentId_ReturnsChildrenGenres()
    {
        Mock<IUnitOfWork> unitOfWork = new();
        unitOfWork.Setup(m => m.GenreRepository.GetByParentIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Guid id) => GenreSeed.GetGenres().Where(g => g.ParentGenreId == id));

        var genreService = new GenreService(unitOfWork.Object);

        var childrenGenres = await genreService.GetByParentIdAsync(GenreSeed.Action.Id);

        Assert.NotNull(childrenGenres);
        Assert.Equivalent(new List<Genre>() { GenreSeed.Shooter }, childrenGenres);
    }

    [Fact]
    public async Task Delete_GivenValidId_DeletesGenre()
    {
        Mock<IUnitOfWork> unitOfWork = GetDummyUnitOfWorkMock();
        Guid id = GenreSeed.GetGenres().First().Id;
        var genreService = new GenreService(unitOfWork.Object);

        await genreService.DeleteAsync(id);

        unitOfWork.Verify(m => m.GenreRepository.DeleteByIdAsync(id), Times.Once());
        unitOfWork.Verify(m => m.SaveChangesAsync(), Times.Once());
    }

    [Fact]
    public async Task Create_GivenValidGenre_CreatesGenre()
    {
        Mock<IUnitOfWork> unitOfWork = GetDummyUnitOfWorkMock();
        var validGenre = new Genre() { Name = "Adventure" };
        var genreService = new GenreService(unitOfWork.Object);

        await genreService.CreateAsync(validGenre);

        unitOfWork.Verify(
            m => m.GenreRepository.InsertAsync(It.Is<Genre>(p => p.Name == validGenre.Name)),
            Times.Once());

        unitOfWork.Verify(m => m.SaveChangesAsync(), Times.Once());
    }

    [Fact]
    public async Task Update_GivenValidGenre_UpdatesGenre()
    {
        Mock<IUnitOfWork> unitOfWork = GetDummyUnitOfWorkMock();
        var genre = new Genre() { Name = "Adventure" };
        var genreService = new GenreService(unitOfWork.Object);

        await genreService.UpdateAsync(genre);

        unitOfWork.Verify(
            m => m.GenreRepository.Update(It.Is<Genre>(p => p.Name == genre.Name)),
            Times.Once());

        unitOfWork.Verify(m => m.SaveChangesAsync(), Times.Once());
    }

    [Fact]
    public async Task GetByGameKey_GivenValidKey_ReturnsGenres()
    {
        Mock<IUnitOfWork> unitOfWork = GetDummyUnitOfWorkMock();
        var genreService = new GenreService(unitOfWork.Object);
        const string gameKey = "GearsOfWar";

        var genres = await genreService.GetByGameKeyAsync(gameKey);

        Assert.NotNull(genres);
    }

    private static Mock<IUnitOfWork> GetDummyUnitOfWorkMock()
    {
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        mockUnitOfWork.Setup(m => m.GenreRepository.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(GenreSeed.Action);

        mockUnitOfWork.Setup(m => m.GenreRepository.GetAllAsync())
            .ReturnsAsync(GenreSeed.GetGenres());

        mockUnitOfWork.Setup(m => m.GenreRepository.GetByGameKeyAsync(It.IsAny<string>()))
            .ReturnsAsync(GenreSeed.GetGenres());

        return mockUnitOfWork;
    }
}