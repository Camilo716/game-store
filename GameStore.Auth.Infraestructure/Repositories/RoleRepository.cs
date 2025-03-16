using AutoMapper;
using GameStore.Auth.Core.Role;
using GameStore.Auth.Infraestructure.Data;
using GameStore.Auth.Infraestructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Auth.Infraestructure.Repositories;

public class RoleRepository(GameStoreAuthDbContext dbContext, IMapper mapper)
    : BaseRepository<Role>(dbContext),
    IRoleRepository
{
    public async Task InsertAsync(CreateRoleRequest createRoleRequest)
    {
        Role role = mapper.Map<Role>(createRoleRequest.Role);

        var privileges = await DbContext.Privileges
            .Where(p => createRoleRequest.Permissions.Contains(p.Id))
            .ToListAsync();

        role.Privileges = privileges;
        role.NormalizedName = role.Name.Trim().ToUpperInvariant();

        await InsertAsync(role);
    }

    public async Task UpdateAsync(CreateRoleRequest roleUpdateRequest)
    {
        Role role = await DbSet
            .Include(p => p.Privileges)
            .FirstOrDefaultAsync(r => r.Id == roleUpdateRequest.Role.Id)
            ?? throw new InvalidOperationException($"Role {roleUpdateRequest.Role.Id} not found.");

        var privileges = await DbContext.Privileges
            .Where(p => roleUpdateRequest.Permissions.Contains(p.Id))
            .ToListAsync();

        role.Name = roleUpdateRequest.Role.Name;
        role.NormalizedName = role.Name.Trim().ToUpperInvariant();

        role.Privileges.Clear();
        role.Privileges = privileges;

        Update(role);
    }
}