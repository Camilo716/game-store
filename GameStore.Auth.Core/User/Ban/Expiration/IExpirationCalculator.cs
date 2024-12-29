namespace GameStore.Auth.Core.User.Ban.Expiration;

public interface IExpirationCalculator
{
    BanExpiration Calculate(UserBanDuration userBanDuration);
}