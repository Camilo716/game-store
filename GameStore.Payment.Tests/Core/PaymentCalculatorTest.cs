using GameStore.Payment.Core.Dtos;
using GameStore.Payment.Core.Models;
using GameStore.Payment.Core.Services;
using GameStore.Payment.Core.Services.Payment;

namespace GameStore.Payment.Tests.Core;

public class PaymentCalculatorTest
{
    [Fact]
    public void CalculatePayment_WithoutDiscount_CorrectlyCalculateValues()
    {
        Order order = new()
        {
            OrderGames =
            [
                new OrderGame()
                {
                    Price = 10.0,
                    Quantity = 2,
                    Discount = 0,
                },
                new OrderGame()
                {
                    Price = 20.0,
                    Quantity = 1,
                    Discount = 0,
                },
            ],
        };
        PaymentCalculationService paymentCalculationService = new(new DateTimeProvider());

        PaymentResponse paymentContext = paymentCalculationService.Calculate(order);

        Assert.Equal(40.0, paymentContext.Total);
    }
}