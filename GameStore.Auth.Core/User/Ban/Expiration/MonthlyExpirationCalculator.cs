using GameStore.Auth.Core.Date;

namespace GameStore.Auth.Core.User.Ban.Expiration;

public class MonthlyExpirationCalculator(
    IDateTimeProvider dateTimeProvider)
    : IExpirationCalculator
{
    public BanExpiration Calculate(UserBanDuration userBanDuration)
    {
        return new BanExpiration()
        {
            Date = dateTimeProvider.Now().AddMonths(userBanDuration.Value),
        };
    }
}