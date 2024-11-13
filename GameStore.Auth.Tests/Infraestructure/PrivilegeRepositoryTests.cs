using GameStore.Auth.Infraestructure.Data;
using GameStore.Auth.Tests.Seed;

namespace GameStore.Auth.Tests.Infraestructure;

public class PrivilegeRepositoryTests
{
    [Fact]
    public async Task GetAll_ReturnsPrivilegesInDatabase()
    {
        using var dbContext = UnitTestHelper.GetUnitTestDbContext();
        var unitOfWork = new UnitOfWork(dbContext, Mapper.Create());

        var privilege = await unitOfWork.PrivilegeRepository.GetAllAsync();

        Assert.NotNull(privilege);
        Assert.Equal(PrivilegeSeed.GetPrivileges().Count, privilege.Count());
    }
}