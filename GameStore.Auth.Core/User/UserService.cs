using System.Security.Authentication;
using GameStore.Auth.Core.ProcessResult;
using GameStore.Auth.Core.Role;
using GameStore.Auth.Core.User.Login;

namespace GameStore.Auth.Core.User;

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

    public async Task DeleteByIdAsync(string id)
    {
        await userManager.DeleteByIdAsync(id);
    }

    public async Task<IEnumerable<UserModel>> GetAllAsync()
    {
        return await userManager.GetAllAsync();
    }

    public async Task<IEnumerable<RoleModel>> GetUserRolesAsync(string id)
    {
        return await userManager.GetUserRolesAsync(id);
    }

    public async Task<AuthToken> LoginAsync(LoginRequest loginRequest)
    {
        UserModel userModel = await userManager.FindByNameAsync(loginRequest.Login)
            ?? throw new AuthenticationException();

        Result result = await signInManager.PasswordSignInAsync(
            userModel, loginRequest.Password, isPersistent: false, lockoutOnFailure: false);

        return !result.Success
            ? throw new AuthenticationException(result.Errors.ToString())
            : await tokenGenerator.GenerateTokenAsync(userModel);
    }

    public async Task<Result> UpdateAsync(CreateUserRequest updateUserRequest)
    {
        UserModel user = updateUserRequest.User;

        Result createResult = await userManager.UpdateAsync(user, updateUserRequest.Password);

        if (!createResult.Success)
        {
            return createResult;
        }

        Result updateRolesResult = await UpdateUserRolesAsync(updateUserRequest, user);

        return updateRolesResult;
    }

    private async Task<Result> UpdateUserRolesAsync(CreateUserRequest updateUserRequest, UserModel user)
    {
        var currentRolesIds = (await userManager.GetUserRolesAsync(user.Id)).Select(r => r.Id);
        List<string> currentRolesNames = await GetRoleNames(currentRolesIds);
        List<string> newRolesNames = await GetRoleNames(updateUserRequest.Roles);

        var rolesToAdd = newRolesNames.Except(currentRolesNames).ToList();
        var rolesToRemove = currentRolesNames.Except(newRolesNames).ToList();

        Result removeResult = await userManager.RemoveFromRolesAsync(user, rolesToRemove);

        if (!removeResult.Success)
        {
            return removeResult;
        }

        Result addResult = await userManager.AddToRolesAsync(user, rolesToAdd);

        return addResult;
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