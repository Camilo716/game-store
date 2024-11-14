using GameStore.Auth.Core.Dtos;
using GameStore.Auth.Core.Models;

namespace GameStore.Auth.Core.Interfaces;

public interface IUserManager
{
    Task<Result> CreateAsync(UserModel userModel, string password);

    Task<Result> AddToRolesAsync(UserModel userModel, IEnumerable<string> roles);
}