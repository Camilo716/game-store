using GameStore.Payment.Core.Dtos;
using GameStore.Payment.Core.Interfaces;
using GameStore.Payment.Core.Models;

namespace GameStore.Payment.Core.Services.Payment.Strategies;

public class BankPaymentProcessor : IPaymentProcessor
{
    public Task<PaymentResponse> ProcessPaymentAsync(Order order)
    {
        // Implement bank payment
        return Task.FromResult(new PaymentResponse());
    }
}