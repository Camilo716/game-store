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
    public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
    {
        IEnumerable<Order> orders = await OrderService.GetOrdersAsync();
        return Ok(orders);
    }

    [HttpGet]
    [Route("{orderId}/details")]
    public async Task<ActionResult<IEnumerable<OrderGame>>> GetOrderDetails([FromRoute] Guid orderId)
    {
        IEnumerable<OrderGame> orderGames = await OrderService.GetOrderDetailsAsync(orderId);
        return Ok(orderGames);
    }

    [HttpGet]
    [Route("cart")]
    public async Task<ActionResult<IEnumerable<Order>>> GetCart()
    {
        IEnumerable<Order> cart = await OrderService.GetCartAsync();
        return Ok(cart);
    }

    [HttpDelete]
    [Route("cart/{gameKey}")]
    public async Task<ActionResult> DeleteGameFromCart([FromRoute] string gameKey)
    {
        await OrderService.DeleteGameFromCartAsync(gameKey);
        return Ok();
    }
}