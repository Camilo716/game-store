namespace GameStore.Auth.Core.User.Ban.Expiration;

public class PermanentExpirationCalculator
    : IExpirationCalculator
{
    public BanExpiration Calculate(UserBanDuration userBanDuration)
    {
        return new BanExpiration()
        {
            Date = DateTime.MaxValue,
        };
    }
}