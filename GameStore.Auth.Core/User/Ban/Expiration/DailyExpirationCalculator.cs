using GameStore.Auth.Core.Date;

namespace GameStore.Auth.Core.User.Ban.Expiration;

public class DailyExpirationCalculator(
    IDateTimeProvider dateTimeProvider)
    : IExpirationCalculator
{
    public BanExpiration Calculate(UserBanDuration userBanDuration)
    {
        return new BanExpiration()
        {
            Date = dateTimeProvider.Now().AddDays(userBanDuration.Value),
        };
    }
}