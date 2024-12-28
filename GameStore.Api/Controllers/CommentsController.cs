using GameStore.Core.Comment.UserBan;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentsController(
    IUserBanService userBanService)
    : ControllerBase
{
    [HttpGet]
    [Route("ban/durations")]
    public async Task<IActionResult> GetUserBanDurations()
    {
        return await Task.Run(() =>
        {
            var banDurations = userBanService.GetUserBanDurations();
            return Ok(banDurations.Select(d => d.Description));
        });
    }
}