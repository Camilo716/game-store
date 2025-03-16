using GameStore.Auth.Core.Config;
using GameStore.Auth.Core.Privilege;
using GameStore.Auth.Core.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Auth.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesController(
    IPrivilegeService privilegeService,
    IRoleService roleService) : ControllerBase
{
    public IPrivilegeService PrivilegeService => privilegeService;

    [HttpGet]
    [Authorize(Policy = nameof(Permissions.ViewRoles))]
    public async Task<ActionResult<IEnumerable<RoleModel>>> Get()
    {
        var roles = await roleService.GetAllAsync();
        return Ok(roles);
    }

    [HttpPost]
    [Authorize(Policy = nameof(Permissions.AddRole))]
    public async Task<IActionResult> Post([FromBody] CreateRoleRequest createRoleRequest)
    {
        await roleService.InsertAync(createRoleRequest);
        return Ok();
    }

    [HttpPut]
    [Authorize(Policy = nameof(Permissions.UpdateRole))]
    public async Task<IActionResult> Put([FromBody] CreateRoleRequest update)
    {
        await roleService.UpdateAsync(update);
        return Ok();
    }

    [HttpGet]
    [Route("permissions")]
    public async Task<ActionResult<IEnumerable<PrivilegeModel>>> GetPermissions()
    {
        var privileges = await PrivilegeService.GetAllAsync();
        return Ok(privileges);
    }

    [HttpGet]
    [Route("{id}/permissions")]
    public async Task<ActionResult<IEnumerable<PrivilegeModel>>> GetPermissionsByRoleId([FromRoute] string id)
    {
        var privileges = await PrivilegeService.GetByRoleIdAsync(id);
        return Ok(privileges);
    }

    [HttpDelete]
    [Route("{id}")]
    [Authorize(Policy = nameof(Permissions.DeleteRole))]
    public async Task<ActionResult> DeleteAsync([FromRoute] string id)
    {
        await roleService.DeleteByIdAsync(id);
        return NoContent();
    }
}