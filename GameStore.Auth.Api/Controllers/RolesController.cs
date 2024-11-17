using GameStore.Auth.Core.Enums;
using GameStore.Auth.Core.Interfaces;
using GameStore.Auth.Core.Models;
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
    public async Task<ActionResult<IEnumerable<RoleModel>>> Get()
    {
        var roles = await roleService.GetAllAsync();
        return Ok(roles);
    }

    [HttpGet]
    [Route("permissions")]
    [Authorize(Policy = nameof(Permissions.ViewRoles))]
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
    public async Task<ActionResult> DeleteAsync([FromRoute] string id)
    {
        await roleService.DeleteByIdAsync(id);
        return NoContent();
    }
}