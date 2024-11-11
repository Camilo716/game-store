using GameStore.Payment.Core.Dtos;
using GameStore.Payment.Core.Models;

namespace GameStore.Payment.Core.Interfaces;

public interface IPaymentProcessor
{
    Task<PaymentResponse> ProcessPaymentAsync(Order order);
}