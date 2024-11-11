using GameStore.Payment.Core.Enums;

namespace GameStore.Payment.Core.Dtos;

public class PaymentRequest
{
    public PaymentMethods PaymentMethod { get; set; }

    public CardDetails? CardDetails { get; set; }
}