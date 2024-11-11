using GameStore.Payment.Core.Enums;
using GameStore.Payment.Core.Interfaces;
using GameStore.Payment.Core.Services.Payment.Strategies;

namespace GameStore.Payment.Core.Services.Payment;

public class PaymentProcessorFactory : IPaymentProcessorFactory
{
    public IPaymentProcessor CreateProcessor(PaymentMethods method)
    {
        return method switch
        {
            PaymentMethods.Bank => new BankPaymentProcessor(),
            PaymentMethods.IBox => new BoxPaymentProcessor(),
            PaymentMethods.Visa => new VisaPaymentProcessor(),
            _ => throw new ArgumentException("Unsupported payment method"),
        };
    }
}