namespace GameStore.Core.Comment.UserBan;

public class UserBanService : IUserBanService
{
    public IEnumerable<UserBanDuration> GetUserBanDurations()
    {
        return
        [
            new UserBanDuration("hours_1", "1 hour"),
            new UserBanDuration("days_1", "1 day"),
            new UserBanDuration("weeks_1", "1 week"),
            new UserBanDuration("months_1", "1 month"),
            new UserBanDuration("permanent", "Permanent"),
        ];
    }
}