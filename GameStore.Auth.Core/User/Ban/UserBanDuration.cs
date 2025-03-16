namespace GameStore.Auth.Core.User.Ban;

public enum Interval
{
    Hours = 0,
    Days = 1,
    Weeks = 2,
    Months = 3,
    Permanent = 4,
}

public record UserBanDuration(
    Interval Interval,
    int Value,
    string Description);
