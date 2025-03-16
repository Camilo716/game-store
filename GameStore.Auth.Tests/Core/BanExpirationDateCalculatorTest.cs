using GameStore.Auth.Core.Date;
using GameStore.Auth.Core.User.Ban;
using GameStore.Auth.Core.User.Ban.Expiration;
using Moq;

namespace GameStore.Auth.Tests.Core;

public class BanExpirationDateCalculatorTest
{
    [Theory]
    [MemberData(nameof(CalculationTestCases))]
    public void CalculateExpirationDate(UserBanDuration banDuration, DateTime expectedExpirationDate)
    {
        Mock<IDateTimeProvider> dateTimeProvider = new();
        dateTimeProvider.Setup(x => x.Now()).Returns(new DateTime(
            year: 1,
            month: 1,
            day: 1,
            hour: 1,
            minute: 1,
            second: 1));

        var calculator = new ExpirationCalculator(dateTimeProvider.Object);

        BanExpiration expiration = calculator.Calculate(banDuration);

        Assert.Equal(expectedExpirationDate, expiration.Date);
    }

    public static IEnumerable<object[]> CalculationTestCases()
    {
        return
        [
            [
                new UserBanDuration(Interval.Hours, 1, string.Empty),
                new DateTime(1, 1, 1, 2, 1, 1)
            ],
            [
                new UserBanDuration(Interval.Days, 1, string.Empty),
                new DateTime(1, 1, 2, 1, 1, 1)
            ],
            [
                new UserBanDuration(Interval.Weeks, 1, string.Empty),
                new DateTime(1, 1, 8, 1, 1, 1)
            ],
            [
                new UserBanDuration(Interval.Months, 1, string.Empty),
                new DateTime(1, 2, 1, 1, 1, 1)
            ],
            [
                new UserBanDuration(Interval.Permanent, 1, string.Empty),
                DateTime.MaxValue
            ],
        ];
    }
}