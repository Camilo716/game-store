using System.Security.Authentication;
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
        CreateUserRequest createUserRequest = GetValidUserCreationRequest();
        UserService userService = new(
            GetDummyUserManager(), GetDummyRoleManager(), null, GetDummyTokenGenerator());

        Result result = await userService.CreateAsync(createUserRequest);

        Assert.True(result.Success);
    }

    [Fact]
    public async Task Login_GivenUnregisteredUser_ThrowsAuthenticationException()
    {
        LoginRequest loginRequest = new()
        {
            Login = "NotRegistered",
            Password = "123aA!",
            InternalAuth = true,
        };

        Mock<IUserManager> userManager = new();
        userManager.Setup(um => um.FindByNameAsync(It.IsAny<string>())).ReturnsAsync((UserModel)null);

        UserService userService = new(
            userManager.Object, GetDummyRoleManager(), null, GetDummyTokenGenerator());

        Func<Task> loginFunc = async () => { await userService.LoginAsync(loginRequest); };

        await Assert.ThrowsAsync<AuthenticationException>(loginFunc);
    }

    [Fact]
    public async Task Login_GivenInvalidCredentials_ThrowsAuthenticationException()
    {
        LoginRequest loginRequest = new()
        {
            Login = "UserName",
            Password = "WrongPassword",
            InternalAuth = true,
        };

        Mock<ISignInManager> roleManager = new();
        roleManager.Setup(rm => rm
            .PasswordSignInAsync(It.IsAny<UserModel>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(Result.FailureResult([]));

        UserService userService = new(
            GetDummyUserManager(), GetDummyRoleManager(), roleManager.Object, GetDummyTokenGenerator());

        Func<Task> loginFunc = async () => { await userService.LoginAsync(loginRequest); };

        await Assert.ThrowsAsync<AuthenticationException>(loginFunc);
    }

    [Fact]
    public async Task Login_GivenValidCredentials_ReturnsToken()
    {
        LoginRequest loginRequest = new()
        {
            Login = "UserName",
            Password = "CorrectPassword",
            InternalAuth = true,
        };
        UserService userService = new(
            GetDummyUserManager(), GetDummyRoleManager(), GetDummySignInManager(), GetDummyTokenGenerator());

        AuthToken token = await userService.LoginAsync(loginRequest);

        Assert.True(!string.IsNullOrEmpty(token.Token));
    }

    private static CreateUserRequest GetValidUserCreationRequest()
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

    private static IUserManager GetDummyUserManager()
    {
        Mock<IUserManager> userManager = new();

        userManager.Setup(r => r.CreateAsync(It.IsAny<UserModel>(), It.IsAny<string>()))
            .ReturnsAsync(Result.SuccessResult());

        userManager.Setup(r => r.FindByNameAsync(It.IsAny<string>()))
            .ReturnsAsync(UserSeed.UserManagerModel);

        return userManager.Object;
    }

    private static IRoleManager GetDummyRoleManager()
    {
        Mock<IRoleManager> roleManager = new();

        roleManager.Setup(r => r.FindByIdAsync(It.IsAny<string>()))
            .ReturnsAsync(RoleSeed.AdminModel);
        return roleManager.Object;
    }

    private static ISignInManager GetDummySignInManager()
    {
        Mock<ISignInManager> signInManager = new();

        signInManager.Setup(rm => rm
            .PasswordSignInAsync(It.IsAny<UserModel>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(Result.SuccessResult());

        return signInManager.Object;
    }

    private static ITokenGenerator GetDummyTokenGenerator()
    {
        Mock<ITokenGenerator> tokenGenerator = new();

        tokenGenerator.Setup(tg => tg.GenerateToken(It.IsAny<UserModel>()))
            .Returns(new AuthToken() { Token = "AwesomeToken" });

        return tokenGenerator.Object;
    }
}