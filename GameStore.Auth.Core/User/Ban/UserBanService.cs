using GameStore.Auth.Core.UnitOfWork;
using GameStore.Auth.Core.User.Ban.Expiration;

namespace GameStore.Auth.Core.User.Ban;

public class UserBanService(
    IUnitOfWork unitOfWork,
    IExpirationCalculator expirationCalculator) : IUserBanService
{
    public async Task BanUserAsync(string userName, UserBanDuration duration)
    {
        BanExpiration expiration = expirationCalculator.Calculate(duration);
        await unitOfWork.UserRepository.BanUserByUserNameAsync(expiration.Date, userName);
        await unitOfWork.SaveChangesAsync();
    }

    public IEnumerable<UserBanDuration> GetUserBanDurations()
    {
        return
        [
            new UserBanDuration(Interval.Hours, 1, "1 hour"),
            new UserBanDuration(Interval.Days, 1, "1 day"),
            new UserBanDuration(Interval.Weeks, 1, "1 week"),
            new UserBanDuration(Interval.Months, 1, "1 month"),
            new UserBanDuration(Interval.Permanent, 0, "Permanent"),
        ];
    }
}