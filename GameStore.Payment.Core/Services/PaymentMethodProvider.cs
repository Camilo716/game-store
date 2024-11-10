using GameStore.Payment.Core.Interfaces;
using GameStore.Payment.Core.Models;

namespace GameStore.Payment.Core.Services;

public class PaymentMethodProvider : IPaymentMethodProvider
{
    public IEnumerable<PaymentMethod> GetPaymentMethods()
    {
        return
        [
            new() { Title = "Bank", ImageUrl = "payment-methods/bank.webp", Description = "Pay directly from your bank account" },
            new() { Title = "IBox terminal", ImageUrl = "payment-methods/ibox.webp", Description = "Make payments at nearby IBox terminals" },
            new() { Title = "Visa", ImageUrl = "payment-methods/visa.webp", Description = "Secure payment using your Visa card" },
        ];
    }
}