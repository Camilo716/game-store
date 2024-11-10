using GameStore.Core.Models;
using GameStore.Infraestructure.Data;
using GameStore.Infraestructure.Repositories;
using GameStore.Tests.Seed;

namespace GameStore.Tests.Infraestructure;

public class PlatformRepositoryTests
{
    [Fact]
    public async Task GetById_GivenValidId_ReturnsPlatformInDatabase()
    {
        using var dbContext = UnitTestHelper.GetUnitTestDbContext();
        var platformRepository = new PlatformRepository(dbContext);
        Guid id = PlatformSeed.GetPlatforms().First().Id;

        var platform = await platformRepository.GetByIdAsync(id);

        Assert.NotNull(platform);
        Assert.Equal(id, platform.Id);
    }

    [Fact]
    public async Task GetAll_ReturnsPlatformsInDatabase()
    {
        using var dbContext = UnitTestHelper.GetUnitTestDbContext();
        var platformRepository = new PlatformRepository(dbContext);

        var platform = await platformRepository.GetAllAsync();

        Assert.NotNull(platform);
        Assert.Equal(PlatformSeed.GetPlatforms().Count, platform.Count());
    }

    [Fact]
    public async Task Delete_GivenValidId_DeletesPlatformInDatabase()
    {
        using var dbContext = UnitTestHelper.GetUnitTestDbContext();
        var unitOfWork = new UnitOfWork(dbContext);
        Guid id = PlatformSeed.GetPlatforms().First().Id;

        await unitOfWork.PlatformRepository.DeleteByIdAsync(id);
        await unitOfWork.SaveChangesAsync();

        Assert.Equal(PlatformSeed.GetPlatforms().Count - 1, dbContext.Platforms.Count());
    }

    [Fact]
    public async Task Insert_GivenValidPlatform_InsertsPlatformInDatabase()
    {
        using var dbContext = UnitTestHelper.GetUnitTestDbContext();
        var unitOfWork = new UnitOfWork(dbContext);
        var validPlatform = new Platform() { Type = "Virtual Reality" };

        await unitOfWork.PlatformRepository.InsertAsync(validPlatform);
        await unitOfWork.SaveChangesAsync();

        Assert.Equal(PlatformSeed.GetPlatforms().Count + 1, dbContext.Platforms.Count());
    }

    [Fact]
    public async Task Update_GivenValidPlatform_UpdatesPlatformInDatabase()
    {
        using var dbContext = UnitTestHelper.GetUnitTestDbContext();
        var unitOfWork = new UnitOfWork(dbContext);
        var platform = await dbContext.Platforms.FindAsync(PlatformSeed.Mobile.Id);
        platform.Type = "Virtual Reality";

        unitOfWork.PlatformRepository.Update(platform);
        await unitOfWork.SaveChangesAsync();

        var updatedPlatform = await dbContext.Platforms.FindAsync(platform.Id);
        Assert.Equal(platform, updatedPlatform);
    }

    [Fact]
    public async Task GetByGameKey_GivenValidKey_ReturnsPlatformsInDatabase()
    {
        using var dbContext = UnitTestHelper.GetUnitTestDbContext();
        var unitOfWork = new UnitOfWork(dbContext);
        const string gameKey = "GearsOfWar";

        var platforms = await unitOfWork.PlatformRepository.GetByGameKeyAsync(gameKey);

        Assert.NotNull(platforms);
        Assert.All(
            platforms,
            platform => Assert.Contains(
                gameKey,
                platform.Games.Select(game => game.Key)));
    }
}