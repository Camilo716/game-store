using GameStore.Core.Comment.UserBan;

namespace GameStore.Tests.Core;

public class UserBanServiceTest
{
    [Fact]
    public void GetBanDurations_ReturnsDefaultBanDurations()
    {
        var userBanService = new UserBanService();

        IEnumerable<UserBanDuration> durations = userBanService.GetUserBanDurations();

        var durationCodes = durations.Select(d => d.Code);
        Assert.Contains("hours_1", durationCodes);
        Assert.Contains("days_1", durationCodes);
        Assert.Contains("weeks_1", durationCodes);
        Assert.Contains("months_1", durationCodes);
        Assert.Contains("permanent", durationCodes);
    }
}