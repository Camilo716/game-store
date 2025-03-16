namespace GameStore.Auth.Core.Privilege;

public interface IPrivilegeRepository
{
    Task<IEnumerable<PrivilegeModel>> GetAllAsync();

    Task<IEnumerable<PrivilegeModel>> GetByRoleIdAsync(string roleId);
}