using GameStore.Auth.Core.Dtos;
using GameStore.Auth.Core.Models;

namespace GameStore.Auth.Core.Interfaces;

public interface IRoleService
{
    Task<IEnumerable<RoleModel>> GetAllAsync();

    Task DeleteByIdAsync(string id);

    Task InsertAync(CreateRoleRequest createRoleRequest);

    Task UpdateAsync(CreateRoleRequest updateRequest);
}