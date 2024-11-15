using GameStore.Auth.Core.Models;
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

    [Fact]
    public async Task GetByRole_ReturnsAlsoPrivilegesOfChildrenRoles()
    {
        using var dbContext = UnitTestHelper.GetUnitTestDbContext();
        var unitOfWork = new UnitOfWork(dbContext, Mapper.Create());
        string roleId = RoleSeed.Admin.Id;

        IEnumerable<PrivilegeModel> privileges = await unitOfWork.PrivilegeRepository.GetByRoleIdAsync(roleId);

        var privilegesIds = privileges.Select(x => x.Id);
        Assert.Contains(RoleSeed.Admin.Privileges[0].Id, privilegesIds);
        Assert.Contains(RoleSeed.Manager.Privileges[0].Id, privilegesIds);
    }
}