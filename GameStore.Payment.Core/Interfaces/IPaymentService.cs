using GameStore.Payment.Core.Dtos;
using GameStore.Payment.Core.Models;

namespace GameStore.Payment.Core.Interfaces;

public interface IPaymentService
{
    Task<PaymentResponse> HandlePaymentAsync(PaymentRequest request, Order order);
}