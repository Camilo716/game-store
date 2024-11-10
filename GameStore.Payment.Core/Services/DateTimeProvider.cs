using GameStore.Payment.Core.Interfaces;

namespace GameStore.Payment.Core.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now() => DateTime.Now;
}