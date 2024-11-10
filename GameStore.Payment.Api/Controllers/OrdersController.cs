using GameStore.Payment.Core.Interfaces;
using GameStore.Payment.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Payment.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController(
    IPaymentMethodProvider paymentMethodProvider,
    IOrderService orderService)
    : ControllerBase
{
    public IPaymentMethodProvider PaymentMethodProvider => paymentMethodProvider;

    public IOrderService OrderService => orderService;

    [HttpGet]
    [Route("payment-methods")]
    public async Task<ActionResult<IEnumerable<PaymentMethod>>> GetPaymentMethods()
    {
        IEnumerable<PaymentMethod> paymentMethods = await Task.FromResult(
            PaymentMethodProvider.GetPaymentMethods());

        return Ok(paymentMethods);
    }

    [HttpGet]
    [Route("cart")]
    public async Task<ActionResult<IEnumerable<Order>>> GetCartAsync()
    {
        IEnumerable<Order> cart = await OrderService.GetCartAsync();

        return Ok(cart);
    }
}