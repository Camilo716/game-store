using GameStore.Payment.Core.Enums;

namespace GameStore.Payment.Core.Interfaces;

public interface IPaymentProcessorFactory
{
    IPaymentProcessor CreateProcessor(PaymentMethods method);
}