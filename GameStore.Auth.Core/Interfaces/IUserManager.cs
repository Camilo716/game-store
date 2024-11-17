using GameStore.Auth.Core.Dtos;
using GameStore.Auth.Core.Models;

namespace GameStore.Auth.Core.Interfaces;

public interface IUserManager
{
    Task<Result> CreateAsync(UserModel userModel, string password);

    Task<Result> AddToRolesAsync(UserModel userModel, IEnumerable<string> roles);

    Task<UserModel?> FindByNameAsync(string name);

    Task<IEnumerable<UserModel>> GetAllAsync();

    Task DeleteByIdAsync(string id);

    Task<IEnumerable<RoleModel>> GetUserRolesAsync(string id);
}