using GameStore.Auth.Core.ProcessResult;
using GameStore.Auth.Core.Role;

namespace GameStore.Auth.Core.User;

public interface IUserManager
{
    Task<Result> CreateAsync(UserModel userModel, string password);

    Task<Result> UpdateAsync(UserModel userModel, string password);

    Task<Result> AddToRolesAsync(UserModel userModel, IEnumerable<string> roles);

    Task<UserModel?> FindByNameAsync(string name);

    Task<IEnumerable<UserModel>> GetAllAsync();

    Task DeleteByIdAsync(string id);

    Task<IEnumerable<RoleModel>> GetUserRolesAsync(string id);

    Task<Result> RemoveFromRolesAsync(UserModel userModel, List<string> roles);
}