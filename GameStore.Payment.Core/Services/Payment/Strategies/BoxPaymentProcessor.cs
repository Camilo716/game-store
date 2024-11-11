using GameStore.Payment.Core.Dtos;
using GameStore.Payment.Core.Interfaces;
using GameStore.Payment.Core.Models;

namespace GameStore.Payment.Core.Services.Payment.Strategies;

public class BoxPaymentProcessor : IPaymentProcessor
{
    public Task<PaymentResponse> ProcessPaymentAsync(Order order)
    {
        // Implement IBox payment
        return Task.FromResult(new PaymentResponse());
    }
}