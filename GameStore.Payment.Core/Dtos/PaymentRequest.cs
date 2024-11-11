using GameStore.Payment.Core.Enums;

namespace GameStore.Payment.Core.Dtos;

public class PaymentRequest
{
    public PaymentMethod PaymentMethod { get; set; }

    public CardDetails? CardDetails { get; set; }
}