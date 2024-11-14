using System.Security.Authentication;
using GameStore.Auth.Core.Dtos;
using GameStore.Auth.Core.Interfaces;
using GameStore.Auth.Core.Models;

namespace GameStore.Auth.Core.Services;

public class UserService(
    IUserManager userManager,
    IRoleManager roleManager,
    ISignInManager signInManager,
    ITokenGenerator tokenGenerator) : IUserService
{
    public async Task<Result> CreateAsync(CreateUserRequest createUserRequest)
    {
        UserModel user = createUserRequest.User;
        string password = createUserRequest.Password;

        Result result = await userManager.CreateAsync(user, password);

        if (!result.Success)
        {
            return result;
        }

        List<string> roles = await GetRoleNames(createUserRequest.Roles);

        await userManager.AddToRolesAsync(user, roles);

        return Result.SuccessResult();
    }

    public async Task<AuthToken> LoginAsync(LoginRequest loginRequest)
    {
        UserModel userModel = await userManager.FindByNameAsync(loginRequest.Login)
            ?? throw new AuthenticationException();

        Result result = await signInManager.PasswordSignInAsync(
            userModel, loginRequest.Password, isPersistent: false, lockoutOnFailure: false);

        return !result.Success
            ? throw new AuthenticationException(result.Errors.ToString())
            : tokenGenerator.GenerateToken(userModel);
    }

    private async Task<List<string>> GetRoleNames(IEnumerable<string> rolesIds)
    {
        List<string> roles =
        [
        ];

        foreach (var roleId in rolesIds)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null || role.Name is null)
            {
                throw new InvalidOperationException($"Role with ID {roleId} not found.");
            }

            roles.Add(role.Name);
        }

        return roles;
    }
}