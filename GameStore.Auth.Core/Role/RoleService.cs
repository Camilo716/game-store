using GameStore.Auth.Core.UnitOfWork;

namespace GameStore.Auth.Core.Role;

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

    public async Task UpdateAsync(CreateRoleRequest updateRequest)
    {
        await unitOfWork.RoleRepository.UpdateAsync(updateRequest);
        await unitOfWork.SaveChangesAsync();
    }
}