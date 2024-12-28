namespace GameStore.Auth.Core.Role;

public interface IRoleRepository
{
    Task InsertAsync(CreateRoleRequest createRoleRequest);

    Task UpdateAsync(CreateRoleRequest roleUpdateRequest);
}