using AutoMapper;
using GameStore.Auth.Core.Interfaces;
using GameStore.Auth.Core.Models;
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
}