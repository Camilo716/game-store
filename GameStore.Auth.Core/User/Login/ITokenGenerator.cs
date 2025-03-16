namespace GameStore.Auth.Core.User.Login;

public interface ITokenGenerator
{
    Task<AuthToken> GenerateTokenAsync(UserModel userModel);
}