using GameStore.Auth.Core.Interfaces;
using GameStore.Auth.Core.Models;
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
    public async Task<ActionResult<IEnumerable<PrivilegeModel>>> Get()
    {
        var privileges = await PrivilegeService.GetAllAsync();
        return Ok(privileges);
    }
}