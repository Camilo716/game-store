using GameStore.Auth.Core.Enums;
using GameStore.Auth.Core.Interfaces;
using GameStore.Auth.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Auth.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesController(
    IPrivilegeService privilegeService) : ControllerBase
{
    public IPrivilegeService PrivilegeService => privilegeService;

    [HttpGet]
    [Route("permissions")]
    [Authorize(Policy = nameof(Permissions.ViewRoles))]
    public async Task<ActionResult<IEnumerable<PrivilegeModel>>> Get()
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
}