namespace GameStore.Core.Comment.UserBan;

public interface IUserBanService
{
    IEnumerable<UserBanDuration> GetUserBanDurations();
}