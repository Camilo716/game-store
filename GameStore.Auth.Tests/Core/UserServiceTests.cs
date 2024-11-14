using GameStore.Auth.Core.Dtos;
using GameStore.Auth.Core.Interfaces;
using GameStore.Auth.Core.Models;
using GameStore.Auth.Core.Services;
using GameStore.Auth.Tests.Seed;
using Moq;

namespace GameStore.Auth.Tests.Core;

public class UserServiceTests
{
    [Fact]
    public async Task CreateUser_GivenValidCredentials_CreatesUser()
    {
        CreateUserRequest createUserRequest = CreateValidUserRequest();
        UserService userService = CreateUserService();

        Result result = await userService.CreateAsync(createUserRequest);

        Assert.True(result.Success);
    }

    private static CreateUserRequest CreateValidUserRequest()
    {
        return new()
        {
            User = new UserModel() { UserName = "test" },
            Roles =
            [
                RoleSeed.Admin.Id,
            ],
            Password = "123aA!",
        };
    }

    private static UserService CreateUserService()
    {
        Mock<IRoleManager> roleManager = new();
        Mock<IUserManager> userManager = new();

        roleManager.Setup(r => r.FindByIdAsync(It.IsAny<string>()))
            .ReturnsAsync(RoleSeed.AdminModel);

        userManager.Setup(r => r.CreateAsync(It.IsAny<UserModel>(), It.IsAny<string>()))
            .ReturnsAsync(Result.SuccessResult());

        return new UserService(userManager.Object, roleManager.Object);
    }
}