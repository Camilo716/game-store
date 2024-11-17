using GameStore.Auth.Core.Models;

namespace GameStore.Auth.Core.Interfaces;

public interface IRoleService
{
    Task<IEnumerable<RoleModel>> GetAllAsync();
}