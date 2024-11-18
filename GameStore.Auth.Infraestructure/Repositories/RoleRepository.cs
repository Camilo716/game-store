using AutoMapper;
using GameStore.Auth.Core.Dtos;
using GameStore.Auth.Core.Interfaces;
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

        await InsertAsync(role);
    }
}