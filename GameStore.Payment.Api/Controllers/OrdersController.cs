using GameStore.Payment.Core.Interfaces;
using GameStore.Payment.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Payment.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController(IPaymentMethodProvider paymentMethodProvider) : ControllerBase
{
    public IPaymentMethodProvider PaymentMethodProvider => paymentMethodProvider;

    [HttpGet]
    [Route("payment-methods")]
    public async Task<ActionResult<IEnumerable<PaymentMethod>>> GetPaymentMethods()
    {
        IEnumerable<PaymentMethod> paymentMethods = await Task.FromResult(
            PaymentMethodProvider.GetPaymentMethods());

        return Ok(paymentMethods);
    }
}