using GameStore.Auth.Core.Dtos;
using GameStore.Auth.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Auth.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(IUserService userService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateUserRequest createUserRequest)
    {
        Result result = await userService.CreateAsync(createUserRequest);

        return result.Success ? Ok() : BadRequest(result.Errors);
    }
}
