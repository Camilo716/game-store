using GameStore.Auth.Core.Dtos;
using GameStore.Auth.Core.Models;

namespace GameStore.Auth.Core.Interfaces;

public interface IUserService
{
    Task<Result> CreateAsync(CreateUserRequest createUserRequest);

    Task<AuthToken> LoginAsync(LoginRequest loginRequest);

    Task<IEnumerable<UserModel>> GetAllAsync();
}