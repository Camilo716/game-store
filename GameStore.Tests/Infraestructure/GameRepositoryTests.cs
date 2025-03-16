using GameStore.Core.Game;
using GameStore.Core.Genre;
using GameStore.Core.Publisher;
using GameStore.Infraestructure.Data;
using GameStore.Tests.Seed;

namespace GameStore.Tests.Infraestructure;

public class GameRepositoryTests
{
    [Fact]
    public async Task GetAll_ReturnsGamesInDatabase()
    {
        using var dbContext = UnitTestHelper.GetUnitTestDbContext();
        var unitOfWork = new UnitOfWork(dbContext);

        var game = await unitOfWork.GameRepository.GetAllAsync();

        Assert.NotNull(game);
        Assert.Equal(GameSeed.GetGames().Count, game.Count());
    }

    [Fact]
    public async Task GetById_GivenValidId_ReturnsGamesInDatabase()
    {
        using var dbContext = UnitTestHelper.GetUnitTestDbContext();
        var unitOfWork = new UnitOfWork(dbContext);
        Guid id = GameSeed.GearsOfWar.Id;

        var game = await unitOfWork.GameRepository.GetByIdAsync(id);

        Assert.NotNull(game);
        Assert.Equal(id, game.Id);
    }

    [Fact]
    public async Task GetByKey_GivenValidKey_ReturnsGameInDatabase()
    {
        using var dbContext = UnitTestHelper.GetUnitTestDbContext();
        var unitOfWork = new UnitOfWork(dbContext);
        string key = GameSeed.GearsOfWar.Key;

        var game = await unitOfWork.GameRepository.GetByKeyAsync(key);

        Assert.NotNull(game);
        Assert.Equal(key, game.Key);
    }

    [Fact]
    public async Task GetByPlatformId_GivenValidPlatformId_ReturnsGameInDatabase()
    {
        using var dbContext = UnitTestHelper.GetUnitTestDbContext();
        var unitOfWork = new UnitOfWork(dbContext);
        Guid platformId = PlatformSeed.Console.Id;

        IEnumerable<Game> games = await unitOfWork.GameRepository.GetByPlatformIdAsync(platformId);

        Assert.NotNull(games);
        Assert.All(
            games,
            game => Assert.Contains(platformId, game.Platforms.Select(p => p.Id)));
    }

    [Fact]
    public async Task GetByGenreId_GivenValidGenreId_ReturnsGameInDatabase()
    {
        using var dbContext = UnitTestHelper.GetUnitTestDbContext();
        var unitOfWork = new UnitOfWork(dbContext);
        Guid genreId = GenreSeed.Action.Id;

        IEnumerable<Game> games = await unitOfWork.GameRepository.GetByGenreIdAsync(genreId);

        Assert.NotNull(games);
        Assert.All(
            games,
            game => Assert.Contains(genreId, game.Genres.Select(p => p.Id)));
    }

    [Fact]
    public async Task GetByPublisherId_GivenValidPublisherId_ReturnsGameInDatabase()
    {
        using var dbContext = UnitTestHelper.GetUnitTestDbContext();
        var unitOfWork = new UnitOfWork(dbContext);
        Guid publisherId = PublisherSeed.Activision.Id;

        IEnumerable<Game> games = await unitOfWork.GameRepository.GetByPublisherIdAsync(publisherId);

        Assert.NotNull(games);
        Assert.All(
            games,
            game => Assert.Equal(publisherId, game.PublisherId));
    }

    [Fact]
    public async Task Insert_GivenValidGame_InsertsGameInDatabase()
    {
        using var dbContext = UnitTestHelper.GetUnitTestDbContext();
        var unitOfWork = new UnitOfWork(dbContext);

        Genre genre = await dbContext.Genres.FindAsync(GenreSeed.Action.Id)
            ?? throw new InvalidOperationException("Genre not found in database.");

        Publisher publisher = await dbContext.Publishers.FindAsync(PublisherSeed.Activision.Id)
            ?? throw new InvalidOperationException("Publisher not found in database.");

        var validGame = new Game()
        {
            Name = "Halo 3",
            Key = "Halo3",
            PublisherId = PublisherSeed.Activision.Id,
            Publisher = publisher,
            Genres =
            [
                genre
            ],
        };

        await unitOfWork.GameRepository.InsertAsync(validGame);
        await unitOfWork.SaveChangesAsync();

        Assert.Equal(GameSeed.GetGames().Count + 1, dbContext.Games.Count());
    }

    [Fact]
    public async Task Delete_GivenValidId_DeletesGameInDatabase()
    {
        using var dbContext = UnitTestHelper.GetUnitTestDbContext();
        var unitOfWork = new UnitOfWork(dbContext);
        Guid id = GameSeed.GetGames().First().Id;

        await unitOfWork.GameRepository.DeleteByIdAsync(id);
        await unitOfWork.SaveChangesAsync();

        var expectedGamesInDb = GameSeed.GetGames().Count - 1;
        Assert.Equal(expectedGamesInDb, dbContext.Games.Count());
    }

    [Fact]
    public async Task Update_GivenValidGame_UpdatesGameInDatabase()
    {
        using var dbContext = UnitTestHelper.GetUnitTestDbContext();
        var unitOfWork = new UnitOfWork(dbContext);
        var game = await dbContext.Games.FindAsync(GameSeed.GearsOfWar.Id);
        game.Name = "Gears of War Edited";

        unitOfWork.GameRepository.Update(game);
        await unitOfWork.SaveChangesAsync();

        var updatedGame = await dbContext.Games.FindAsync(game.Id);
        Assert.Equal(game, updatedGame);
    }
}