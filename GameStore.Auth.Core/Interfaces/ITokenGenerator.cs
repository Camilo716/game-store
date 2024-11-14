using GameStore.Auth.Core.Dtos;
using GameStore.Auth.Core.Models;

namespace GameStore.Auth.Core.Interfaces;

public interface ITokenGenerator
{
    AuthToken GenerateToken(UserModel userModel);
}