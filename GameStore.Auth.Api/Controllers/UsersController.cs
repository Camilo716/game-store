using System.Security.Authentication;
using GameStore.Auth.Core.Dtos;
using GameStore.Auth.Core.Enums;
using GameStore.Auth.Core.Interfaces;
using GameStore.Auth.Core.Models;
using GameStore.Auth.Core.User.Ban;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Auth.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(
    IUserService userService,
    IUserBanService userBanService) : ControllerBase
{
    [HttpPost]
    [Authorize(Policy = nameof(Permissions.AddUser))]
    public async Task<IActionResult> Post([FromBody] CreateUserRequest createUserRequest)
    {
        Result result = await userService.CreateAsync(createUserRequest);

        return result.Success ? Ok() : BadRequest(result.Errors);
    }

    [HttpPut]
    [Authorize(Policy = nameof(Permissions.UpdateUser))]
    public async Task<IActionResult> Put([FromBody] CreateUserRequest updateUserRequest)
    {
        Result result = await userService.UpdateAsync(updateUserRequest);

        return result.Success ? Ok() : BadRequest(result.Errors);
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<AuthToken>> Login([FromBody] LoginRequest loginRequest)
    {
        AuthToken token;

        try
        {
            token = await userService.LoginAsync(loginRequest);
        }
        catch (AuthenticationException ex)
        {
            return Unauthorized(ex.Message);
        }

        return Ok(token);
    }

    [HttpGet]
    [Authorize(Policy = nameof(Permissions.ViewUsers))]
    public async Task<ActionResult<IEnumerable<UserModel>>> Get()
    {
        var users = await userService.GetAllAsync();
        return Ok(users);
    }

    [HttpGet]
    [Route("{id}/roles")]
    [Authorize(Policy = nameof(Permissions.ViewUsers))]
    public async Task<ActionResult<IEnumerable<RoleModel>>> GetUserRoles([FromRoute] string id)
    {
        var roles = await userService.GetUserRolesAsync(id);
        return Ok(roles);
    }

    [HttpDelete]
    [Route("{id}")]
    [Authorize(Policy = nameof(Permissions.DeleteUser))]
    public async Task<ActionResult> Delete([FromRoute] string id)
    {
        await userService.DeleteByIdAsync(id);
        return NoContent();
    }

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
