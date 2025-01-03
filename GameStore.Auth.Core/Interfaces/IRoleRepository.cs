using GameStore.Auth.Core.Dtos;

namespace GameStore.Auth.Core.Interfaces;

public interface IRoleRepository
{
    Task InsertAsync(CreateRoleRequest createRoleRequest);

    Task UpdateAsync(CreateRoleRequest roleUpdateRequest);
}