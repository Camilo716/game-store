using GameStore.Auth.Core.Dtos;
using GameStore.Auth.Core.Models;

namespace GameStore.Auth.Core.Interfaces;

public interface ISignInManager
{
    Task<Result> PasswordSignInAsync(UserModel userModel, string password, bool isPersistent, bool lockoutOnFailure);
}