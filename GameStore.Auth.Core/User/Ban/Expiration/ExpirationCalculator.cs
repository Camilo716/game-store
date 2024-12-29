using GameStore.Auth.Core.Date;

namespace GameStore.Auth.Core.User.Ban.Expiration;

public class ExpirationCalculator(
    IDateTimeProvider dateTimeProvider)
    : IExpirationCalculator
{
    public BanExpiration Calculate(UserBanDuration userBanDuration)
    {
        return ExpirationCalculatorFactory
            .Create(userBanDuration.Interval, dateTimeProvider)
            .Calculate(userBanDuration);
    }
}