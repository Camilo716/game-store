using GameStore.Payment.Core.Dtos;
using GameStore.Payment.Core.Interfaces;
using GameStore.Payment.Core.Models;

namespace GameStore.Payment.Core.Services.Payment;

public class PaymentCalculationService(
    DateTimeProvider dateTimeProvider)
    : IPaymentCalculationService
{
    private DateTimeProvider DateTimeProvider => dateTimeProvider;

    public PaymentResponse Calculate(Order order)
    {
        double total = order.OrderGames.Sum(og => og.Quantity * og.Price);

        return new()
        {
            PaymentDate = DateTimeProvider.Now(),
            Total = total,
        };
    }
}