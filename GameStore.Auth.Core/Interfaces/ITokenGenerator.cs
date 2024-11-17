using GameStore.Auth.Core.Dtos;
using GameStore.Auth.Core.Models;

namespace GameStore.Auth.Core.Interfaces;

public interface ITokenGenerator
{
    Task<AuthToken> GenerateTokenAsync(UserModel userModel);
}