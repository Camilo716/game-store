namespace GameStore.Auth.Core.User;

public interface IUserRepository
{
    Task BanUserByUserNameAsync(DateTime expirationDate, string userName);
}