using AutoMapper;
using GameStore.Auth.Core.Dtos;
using GameStore.Auth.Core.Interfaces;
using GameStore.Auth.Core.Models;
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

    public async Task DeleteByIdAsync(string id)
    {
        var user = await userManager.FindByIdAsync(id)
            ?? throw new InvalidOperationException($"User {id} not found");

        await userManager.DeleteAsync(user);
    }

    private static Result Result(IdentityResult result)
    {
        if (result.Succeeded)
        {
            return Core.Dtos.Result.SuccessResult();
        }

        var errors = result.Errors.Select(e => e.Description);
        return Core.Dtos.Result.FailureResult(errors);
    }
}