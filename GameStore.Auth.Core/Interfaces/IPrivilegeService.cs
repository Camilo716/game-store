using GameStore.Auth.Core.Models;

namespace GameStore.Auth.Core.Interfaces;

public interface IPrivilegeService
{
    Task<IEnumerable<PrivilegeModel>> GetAllAsync();
}