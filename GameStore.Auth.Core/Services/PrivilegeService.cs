using GameStore.Auth.Core.Interfaces;
using GameStore.Auth.Core.Models;

namespace GameStore.Auth.Core.Services;

public class PrivilegeService(IUnitOfWork unitOfWork) : IPrivilegeService
{
    public IUnitOfWork UnitOfWork => unitOfWork;

    public async Task<IEnumerable<PrivilegeModel>> GetAllAsync()
    {
        return await UnitOfWork.PrivilegeRepository.GetAllAsync();
    }
}