using AutoMapper;
using GameStore.Auth.Core.Interfaces;
using GameStore.Auth.Core.Models;
using GameStore.Auth.Infraestructure.Data;
using GameStore.Auth.Infraestructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Auth.Infraestructure.Repositories;

public class PrivilegeRepository(GameStoreAuthDbContext dbContext, IMapper mapper)
    : BaseRepository<Privilege>(dbContext),
    IPrivilegeRepository
{
    public IMapper Mapper => mapper;

    async Task<IEnumerable<PrivilegeModel>> IPrivilegeRepository.GetAllAsync()
    {
        var privileges = await GetAllAsync();
        return mapper.Map<IEnumerable<PrivilegeModel>>(privileges);
    }

    public async Task<IEnumerable<PrivilegeModel>> GetByRoleIdAsync(string roleId)
    {
        Role role = await GetRoleWithDetailsAsync(roleId);

        var privileges = new HashSet<Privilege>(role.Privileges);

        await CollectChildPrivileges(role, privileges);

        return mapper.Map<IEnumerable<PrivilegeModel>>(privileges);
    }

    private async Task CollectChildPrivileges(Role role, HashSet<Privilege> privilegeSet)
    {
        foreach (var childRole in role.ChildrenRoles)
        {
            Role childRoleWithDetails = await GetRoleWithDetailsAsync(childRole.Id);

            foreach (var privilege in childRoleWithDetails.Privileges)
            {
                privilegeSet.Add(privilege);
            }

            await CollectChildPrivileges(childRoleWithDetails, privilegeSet);
        }
    }

    private async Task<Role> GetRoleWithDetailsAsync(string roleId)
    {
        return await DbContext.Roles
            .Include(r => r.Privileges)
            .Include(r => r.ChildrenRoles)
            .FirstOrDefaultAsync(r => r.Id == roleId)
            ?? throw new InvalidOperationException($"Role {roleId} not found.");
    }
}