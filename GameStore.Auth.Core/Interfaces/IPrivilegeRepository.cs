using GameStore.Auth.Core.Models;

namespace GameStore.Auth.Core.Interfaces;

public interface IPrivilegeRepository
{
    Task<IEnumerable<PrivilegeModel>> GetAllAsync();
}