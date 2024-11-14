using GameStore.Auth.Core.Dtos;
using GameStore.Auth.Core.Interfaces;
using GameStore.Auth.Core.Models;
using GameStore.Auth.Infraestructure.Data;
using GameStore.Auth.Infraestructure.Entities;
using Microsoft.AspNetCore.Identity;

namespace GameStore.Auth.Infraestructure.Adapters;

public class SignInManagerIdentityAdapter(
    SignInManager<User> signInManager,
    GameStoreAuthDbContext dbContext) : ISignInManager
{
    public async Task<Result> PasswordSignInAsync(UserModel userModel, string password, bool isPersistent, bool lockoutOnFailure)
    {
        User user = await dbContext.Users.FindAsync(userModel.Id)
            ?? throw new InvalidOperationException($"User {userModel.Id} not found.");

        SignInResult signInResult = await signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure);

        return signInResult.Succeeded ? Result.SuccessResult() : Result.FailureResult([]);
    }
}