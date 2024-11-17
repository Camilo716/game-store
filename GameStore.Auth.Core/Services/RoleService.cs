using GameStore.Auth.Core.Interfaces;
using GameStore.Auth.Core.Models;

namespace GameStore.Auth.Core.Services;

public class RoleService(IRoleManager roleManager) : IRoleService
{
    public async Task<IEnumerable<RoleModel>> GetAllAsync()
    {
        return await roleManager.GetAllAsync();
    }
}