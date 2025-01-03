using GameStore.Auth.Core.Models;

namespace GameStore.Auth.Core.Interfaces;

public interface IRoleManager
{
    Task<RoleModel?> FindByIdAsync(string id);

    Task<IEnumerable<RoleModel>> GetAllAsync();

    Task DeleteByIdAsync(string id);
}