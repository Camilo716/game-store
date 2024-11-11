using GameStore.Payment.Core.Enums;
using GameStore.Payment.Core.Interfaces;
using GameStore.Payment.Core.Services.Payment.Strategies;

namespace GameStore.Payment.Core.Services.Payment;

public class PaymentProcessorFactory : IPaymentProcessorFactory
{
    public IPaymentProcessor CreateProcessor(PaymentMethod method)
    {
        return method switch
        {
            PaymentMethod.Bank => new BankPaymentProcessor(),
            PaymentMethod.IBox => new BoxPaymentProcessor(),
            PaymentMethod.Visa => new VisaPaymentProcessor(),
            _ => throw new ArgumentException("Unsupported payment method"),
        };
    }
}