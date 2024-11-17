using GameStore.Auth.Core.Dtos;
using GameStore.Auth.Core.Models;

namespace GameStore.Auth.Core.Interfaces;

public interface IUserService
{
    Task<Result> CreateAsync(CreateUserRequest createUserRequest);

    Task<AuthToken> LoginAsync(LoginRequest loginRequest);

    Task<IEnumerable<UserModel>> GetAllAsync();

    Task DeleteByIdAsync(string id);

    Task<IEnumerable<RoleModel>> GetUserRolesAsync(string id);

    Task<Result> UpdateAsync(CreateUserRequest updateUserRequest);
}