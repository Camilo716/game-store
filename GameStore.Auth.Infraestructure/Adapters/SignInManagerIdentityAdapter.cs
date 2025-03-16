using GameStore.Auth.Core.ProcessResult;
using GameStore.Auth.Core.User;
using GameStore.Auth.Core.User.Login;
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