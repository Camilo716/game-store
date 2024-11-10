using GameStore.Payment.Core.Models;

namespace GameStore.Payment.Core.Interfaces;

public interface IPaymentMethodProvider
{
    IEnumerable<PaymentMethod> GetPaymentMethods();
}