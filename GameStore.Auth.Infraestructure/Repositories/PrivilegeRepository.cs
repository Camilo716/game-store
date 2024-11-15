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
        var role = await DbContext.Roles
            .Include(r => r.Privileges)
            .FirstOrDefaultAsync(r => r.Id == roleId)
            ?? throw new InvalidOperationException($"Role {roleId} not found.");

        var privileges = new HashSet<Privilege>(role.Privileges);

        CollectChildPrivileges(role, privileges);

        return mapper.Map<IEnumerable<PrivilegeModel>>(privileges);
    }

    private static void CollectChildPrivileges(Role role, HashSet<Privilege> privilegeSet)
    {
        foreach (var childRole in role.ChildrenRoles)
        {
            foreach (var privilege in childRole.Privileges)
            {
                privilegeSet.Add(privilege);
            }

            CollectChildPrivileges(childRole, privilegeSet);
        }
    }
}