namespace GameStore.Auth.Core.User.Ban;

public enum Interval
{
    Hours,
    Days,
    Weeks,
    Months,
    Permanent,
}

public record UserBanDuration(
    Interval Interval,
    int Value,
    string Description);
