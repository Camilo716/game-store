using GameStore.Auth.Core.Dtos;

namespace GameStore.Auth.Core.Interfaces;

public interface IUserService
{
    Task<Result> CreateAsync(CreateUserRequest createUserRequest);

    Task<AuthToken> LoginAsync(LoginRequest loginRequest);
}