using GameStore.Auth.Core.ProcessResult;

namespace GameStore.Auth.Core.User.Login;

public interface ISignInManager
{
    Task<Result> PasswordSignInAsync(UserModel userModel, string password, bool isPersistent, bool lockoutOnFailure);
}