using GameStore.Auth.Core.Privilege;
using GameStore.Auth.Core.UnitOfWork;
using GameStore.Auth.Tests.Seed;
using Moq;

namespace GameStore.Auth.Tests.Core;

public class PrivilegeServiceTests
{
    [Fact]
    public async Task GetAll_ReturnsPrivilegesModels()
    {
        Mock<IUnitOfWork> unitOfWork = GetDummyUnitOfWorkMock();
        var privilegeService = new PrivilegeService(unitOfWork.Object);

        var privileges = await privilegeService.GetAllAsync();

        Assert.NotNull(privileges);
        Assert.Equal(PrivilegeSeed.GetPrivileges().Count, privileges.Count());
    }

    private static Mock<IUnitOfWork> GetDummyUnitOfWorkMock()
    {
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        mockUnitOfWork.Setup(m => m.PrivilegeRepository.GetAllAsync())
            .ReturnsAsync(PrivilegeSeed.GetPrivilegeModels());

        return mockUnitOfWork;
    }
}