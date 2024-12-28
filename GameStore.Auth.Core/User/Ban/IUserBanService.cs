namespace GameStore.Auth.Core.User.Ban;

public interface IUserBanService
{
    IEnumerable<UserBanDuration> GetUserBanDurations();
}