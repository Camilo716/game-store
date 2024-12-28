namespace GameStore.Auth.Core.Privilege;

public interface IPrivilegeService
{
    Task<IEnumerable<PrivilegeModel>> GetAllAsync();

    Task<IEnumerable<PrivilegeModel>> GetByRoleIdAsync(string roleId);
}