using GameStore.Auth.Core.Dtos;
using GameStore.Auth.Core.Models;
using GameStore.Auth.Infraestructure.Data;
using GameStore.Auth.Tests.Seed;

namespace GameStore.Auth.Tests.Infraestructure;

public class RoleRepositoryTests
{
    [Fact]
    public async Task Insert_GivenValidRole_InsertsRoleInDatabase()
    {
        using var dbContext = UnitTestHelper.GetUnitTestDbContext();
        var unitOfWork = new UnitOfWork(dbContext, Mapper.Create());

        var validRole = new CreateRoleRequest()
        {
            Role = new RoleModel() { Name = "NewRole" },
            Permissions =
            [
                PrivilegeSeed.ViewGame.Id
            ],
        };
        Assert.Equal(RoleSeed.GetRoles().Count, dbContext.Roles.Count());

        await unitOfWork.RoleRepository.InsertAsync(validRole);
        await unitOfWork.SaveChangesAsync();

        Assert.Equal(RoleSeed.GetRoles().Count + 1, dbContext.Roles.Count());
    }

    [Fact]
    public async Task Update_GivenValidRole_UpdatesRoleInDatabase()
    {
        using var dbContext = UnitTestHelper.GetUnitTestDbContext();
        var unitOfWork = new UnitOfWork(dbContext, Mapper.Create());

        var roleUpdateRequest = new CreateRoleRequest
        {
            Role = new RoleModel { Id = RoleSeed.Admin.Id, Name = "AdminUpdated" },
            Permissions =
            [
                PrivilegeSeed.AddGame.Id,
            ],
        };

        unitOfWork.RoleRepository.Update(roleUpdateRequest);
        await unitOfWork.SaveChangesAsync();

        var updatedRole = await dbContext.Roles.FindAsync(roleUpdateRequest.Role.Id);
        Assert.Equal(roleUpdateRequest.Role.Name, updatedRole.Name);
    }
}