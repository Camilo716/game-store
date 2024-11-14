using AutoMapper;
using GameStore.Auth.Core.Interfaces;
using GameStore.Auth.Core.Models;
using GameStore.Auth.Infraestructure.Entities;
using Microsoft.AspNetCore.Identity;

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
}