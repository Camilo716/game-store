using AutoMapper;
using GameStore.Auth.Core.ProcessResult;
using GameStore.Auth.Core.Role;
using GameStore.Auth.Core.User;
using GameStore.Auth.Infraestructure.Data;
using GameStore.Auth.Infraestructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Auth.Infraestructure.Adapters;

public class UserManagerIdentityAdapter(
    UserManager<User> userManager,
    IMapper mapper,
    GameStoreAuthDbContext dbContext)
    : IUserManager
{
    public async Task<Result> CreateAsync(UserModel userModel, string password)
    {
        User user = mapper.Map<User>(userModel);
        IdentityResult result = await userManager.CreateAsync(user, password);
        return Result(result);
    }

    public async Task<Result> AddToRolesAsync(UserModel userModel, IEnumerable<string> roles)
    {
        User user = await dbContext.Users.FindAsync(userModel.Id)
            ?? throw new InvalidOperationException($"User {userModel.Id} not found");

        IdentityResult result = await userManager.AddToRolesAsync(user, roles);

        return Result(result);
    }

    public async Task<Result> RemoveFromRolesAsync(UserModel userModel, List<string> roles)
    {
        User user = await dbContext.Users.FindAsync(userModel.Id)
            ?? throw new InvalidOperationException($"User {userModel.Id} not found");

        IdentityResult result = await userManager.RemoveFromRolesAsync(user, roles);

        return Result(result);
    }

    public async Task<UserModel?> FindByNameAsync(string name)
    {
        User? user = await userManager.FindByNameAsync(name);
        return user is null ? null : mapper.Map<UserModel>(user);
    }

    public async Task<IEnumerable<UserModel>> GetAllAsync()
    {
        var users = await userManager.Users.ToListAsync();

        return mapper.Map<IEnumerable<UserModel>>(users);
    }

    public async Task<IEnumerable<RoleModel>> GetUserRolesAsync(string id)
    {
        var user = await userManager.FindByIdAsync(id)
            ?? throw new InvalidOperationException($"User {id} not found");

        IList<string> roles = await userManager.GetRolesAsync(user);

        var dbRoles = await dbContext.Roles
            .Where(r => roles.Contains(r.Name!))
            .ToListAsync();

        return mapper.Map<IEnumerable<RoleModel>>(dbRoles);
    }

    public async Task DeleteByIdAsync(string id)
    {
        var user = await userManager.FindByIdAsync(id)
            ?? throw new InvalidOperationException($"User {id} not found");

        await userManager.DeleteAsync(user);
    }

    public async Task<Result> UpdateAsync(UserModel userModel, string password)
    {
        User user = await dbContext.Users.FindAsync(userModel.Id)
            ?? throw new InvalidOperationException($"User {userModel.Id} not found");

        user.PasswordHash = new PasswordHasher<User>().HashPassword(user, password);
        user.UserName = userModel.UserName;

        IdentityResult result = await userManager.UpdateAsync(user);

        return Result(result);
    }

    private static Result Result(IdentityResult result)
    {
        if (result.Succeeded)
        {
            return Core.ProcessResult.Result.SuccessResult();
        }

        var errors = result.Errors.Select(e => e.Description);
        return Core.ProcessResult.Result.FailureResult(errors);
    }
}