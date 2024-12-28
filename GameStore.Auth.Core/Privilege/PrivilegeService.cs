using GameStore.Auth.Core.UnitOfWork;

namespace GameStore.Auth.Core.Privilege;

public class PrivilegeService(IUnitOfWork unitOfWork) : IPrivilegeService
{
    public IUnitOfWork UnitOfWork => unitOfWork;

    public async Task<IEnumerable<PrivilegeModel>> GetAllAsync()
    {
        return await UnitOfWork.PrivilegeRepository.GetAllAsync();
    }

    public async Task<IEnumerable<PrivilegeModel>> GetByRoleIdAsync(string roleId)
    {
        return await UnitOfWork.PrivilegeRepository.GetByRoleIdAsync(roleId);
    }
}