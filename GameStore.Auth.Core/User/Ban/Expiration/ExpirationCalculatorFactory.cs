using GameStore.Auth.Core.Date;

namespace GameStore.Auth.Core.User.Ban.Expiration;

public static class ExpirationCalculatorFactory
{
    public static IExpirationCalculator Create(Interval interval, IDateTimeProvider dateTimeProvider)
    {
        return interval switch
        {
            Interval.Hours => new HourlyExpirationCalculator(dateTimeProvider),
            Interval.Days => new DailyExpirationCalculator(dateTimeProvider),
            Interval.Weeks => new WeeklyExpirationCalculator(dateTimeProvider),
            Interval.Months => new MonthlyExpirationCalculator(dateTimeProvider),
            Interval.Permanent => new PermanentExpirationCalculator(),
            _ => throw new NotImplementedException(),
        };
    }
}