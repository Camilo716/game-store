namespace GameStore.Auth.Core.User.Ban;

public interface IUserBanService
{
    Task BanUserAsync(string userName, UserBanDuration duration);

    IEnumerable<UserBanDuration> GetUserBanDurations();
}