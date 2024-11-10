using GameStore.Payment.Core.Services;

namespace GameStore.Payment.Tests.Core;

public class PaymentMethodProviderTests
{
    [Fact]
    public void GetPaymentMethods_ReturnsConfiguredPaymentMethods()
    {
        PaymentMethodProvider paymentMethodProvider = new();

        var paymentMethods = paymentMethodProvider.GetPaymentMethods();

        var paymentMethodTitles = paymentMethods.Select(p => p.Title);
        Assert.Contains("Bank", paymentMethodTitles);
        Assert.Contains("IBox terminal", paymentMethodTitles);
        Assert.Contains("Visa", paymentMethodTitles);
    }
}