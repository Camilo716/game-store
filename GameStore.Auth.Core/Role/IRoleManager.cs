namespace GameStore.Auth.Core.Role;

public interface IRoleManager
{
    Task<RoleModel?> FindByIdAsync(string id);

    Task<IEnumerable<RoleModel>> GetAllAsync();

    Task DeleteByIdAsync(string id);
}