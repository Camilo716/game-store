using GameStore.Auth.Core.ProcessResult;
using GameStore.Auth.Core.Role;
using GameStore.Auth.Core.User.Login;

namespace GameStore.Auth.Core.User;

public interface IUserService
{
    Task<Result> CreateAsync(CreateUserRequest createUserRequest);

    Task<AuthToken> LoginAsync(LoginRequest loginRequest);

    Task<IEnumerable<UserModel>> GetAllAsync();

    Task DeleteByIdAsync(string id);

    Task<IEnumerable<RoleModel>> GetUserRolesAsync(string id);

    Task<Result> UpdateAsync(CreateUserRequest updateUserRequest);
}