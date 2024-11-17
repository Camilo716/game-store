using System.Security.Authentication;
using GameStore.Auth.Core.Dtos;
using GameStore.Auth.Core.Interfaces;
using GameStore.Auth.Core.Models;
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

    [HttpPut]
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
    public async Task<ActionResult<IEnumerable<UserModel>>> Get()
    {
        var users = await userService.GetAllAsync();
        return Ok(users);
    }

    [HttpGet]
    [Route("{id}/roles")]
    public async Task<ActionResult<IEnumerable<RoleModel>>> GetUserRoles([FromRoute] string id)
    {
        var roles = await userService.GetUserRolesAsync(id);
        return Ok(roles);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> Delete([FromRoute] string id)
    {
        await userService.DeleteByIdAsync(id);
        return NoContent();
    }
}
