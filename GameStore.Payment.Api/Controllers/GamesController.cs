using GameStore.Payment.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Payment.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GamesController(
    IOrderService orderService)
    : ControllerBase
{
    public IOrderService OrderService => orderService;

    [HttpPost]
    [Route("{gameId}/buy")]
    public async Task<ActionResult> AddGameToCart([FromRoute] Guid gameId)
    {
        await OrderService.AddGameToCartAsync(gameId);
        return Ok(gameId);
    }
}