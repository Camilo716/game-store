using GameStore.Payment.Core.Dtos;
using GameStore.Payment.Core.Interfaces;
using GameStore.Payment.Core.Models;

namespace GameStore.Payment.Core.Services.Payment;

public class PaymentService(IPaymentProcessorFactory processorFactory) : IPaymentService
{
    private IPaymentProcessorFactory ProcessorFactory => processorFactory;

    public async Task<PaymentResponse> HandlePaymentAsync(PaymentRequest request, Order order)
    {
        IPaymentProcessor processor = ProcessorFactory.CreateProcessor(request.PaymentMethod);
        return await processor.ProcessPaymentAsync(order);
    }
}