using GameStore.Auth.Core.Date;

namespace GameStore.Auth.Core.User.Ban.Expiration;

public class WeeklyExpirationCalculator(
    IDateTimeProvider dateTimeProvider)
    : IExpirationCalculator
{
    private const int DaysOfTheWeek = 7;

    public BanExpiration Calculate(UserBanDuration userBanDuration)
    {
        return new BanExpiration()
        {
            Date = dateTimeProvider.Now().AddDays(userBanDuration.Value * DaysOfTheWeek),
        };
    }
}