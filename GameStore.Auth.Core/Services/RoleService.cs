using GameStore.Auth.Core.Dtos;
using GameStore.Auth.Core.Interfaces;
using GameStore.Auth.Core.Models;

namespace GameStore.Auth.Core.Services;

public class RoleService(IRoleManager roleManager, IUnitOfWork unitOfWork) : IRoleService
{
    public async Task DeleteByIdAsync(string id)
    {
        await roleManager.DeleteByIdAsync(id);
    }

    public async Task<IEnumerable<RoleModel>> GetAllAsync()
    {
        return await roleManager.GetAllAsync();
    }

    public async Task InsertAync(CreateRoleRequest createRoleRequest)
    {
        await unitOfWork.RoleRepository.InsertAsync(createRoleRequest);
        await unitOfWork.SaveChangesAsync();
    }
}