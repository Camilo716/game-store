using GameStore.Auth.Core.Dtos;
using GameStore.Auth.Core.Interfaces;
using GameStore.Auth.Core.Models;

namespace GameStore.Auth.Core.Services;

public class UserService(IUserManager userManager, IRoleManager roleManager) : IUserService
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