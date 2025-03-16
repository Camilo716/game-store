using GameStore.Auth.Core.Date;

namespace GameStore.Auth.Core.User.Ban.Expiration;

public class HourlyExpirationCalculator(
    IDateTimeProvider dateTimeProvider)
    : IExpirationCalculator
{
    public BanExpiration Calculate(UserBanDuration userBanDuration)
    {
        return new BanExpiration()
        {
            Date = dateTimeProvider.Now().AddHours(userBanDuration.Value),
        };
    }
}