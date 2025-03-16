using GameStore.Auth.Infraestructure.Data;
using GameStore.Auth.Tests.Seed;

namespace GameStore.Auth.Tests.Infraestructure;

public class UserRepositoryTests
{
    [Fact]
    public async Task BanUserByUserName_SetsExpirationDate()
    {
        using var dbContext = UnitTestHelper.GetUnitTestDbContext();
        var unitOfWork = new UnitOfWork(dbContext, Mapper.Create());
        var expirationDate = new DateTime(1, 1, 1);
        string userName = UserSeed.UserManager.UserName;
        string userId = UserSeed.UserManager.Id;

        await unitOfWork.UserRepository.BanUserByUserNameAsync(expirationDate, userName!);
        await unitOfWork.SaveChangesAsync();

        var updateduser = await dbContext.Users.FindAsync(userId);
        Assert.Equal(expirationDate, updateduser.BanExpirationDate);
    }
}