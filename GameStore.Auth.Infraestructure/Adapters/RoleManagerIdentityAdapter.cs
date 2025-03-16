using AutoMapper;
using GameStore.Auth.Core.Role;
using GameStore.Auth.Infraestructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Auth.Infraestructure.Adapters;

public class RoleManagerIdentityAdapter(
    RoleManager<Role> roleManager,
    IMapper mapper)
    : IRoleManager
{
    public async Task<RoleModel?> FindByIdAsync(string id)
    {
        Role role = await roleManager.FindByIdAsync(id);
        return mapper.Map<RoleModel>(role);
    }

    public async Task<IEnumerable<RoleModel>> GetAllAsync()
    {
        var roles = await roleManager.Roles.ToListAsync();
        return mapper.Map<IEnumerable<RoleModel>>(roles);
    }

    public async Task DeleteByIdAsync(string id)
    {
        var role = await roleManager.FindByIdAsync(id)
            ?? throw new InvalidOperationException($"Role {id} not found.");

        await roleManager.DeleteAsync(role);
    }
}