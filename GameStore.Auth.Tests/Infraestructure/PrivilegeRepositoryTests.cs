using GameStore.Auth.Infraestructure.Repositories;
using GameStore.Auth.Tests.Seed;

namespace GameStore.Auth.Tests.Infraestructure;

public class PrivilegeRepositoryTests
{
    [Fact]
    public async Task GetAll_ReturnsPrivilegesInDatabase()
    {
        using var dbContext = UnitTestHelper.GetUnitTestDbContext();
        var privilegeRepository = new PrivilegeRepository(dbContext);

        var privilege = await privilegeRepository.GetAllAsync();

        Assert.NotNull(privilege);
        Assert.Equal(PrivilegeSeed.GetPrivileges().Count, privilege.Count());
    }
}