namespace GameStore.Auth.Core.Role;

public interface IRoleService
{
    Task<IEnumerable<RoleModel>> GetAllAsync();

    Task DeleteByIdAsync(string id);

    Task InsertAync(CreateRoleRequest createRoleRequest);

    Task UpdateAsync(CreateRoleRequest updateRequest);
}