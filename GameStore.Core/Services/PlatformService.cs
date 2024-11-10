using GameStore.Core.Interfaces;
using GameStore.Core.Models;

namespace GameStore.Core.Services;

public class PlatformService(IUnitOfWork unitOfWork) : IPlatformService
{
    private IUnitOfWork UnitOfWork => unitOfWork;

    public async Task<IEnumerable<Platform>> GetAllAsync()
    {
        return await UnitOfWork.PlatformRepository.GetAllAsync();
    }

    public async Task<Platform> GetByIdAsync(Guid id)
    {
        return await UnitOfWork.PlatformRepository.GetByIdAsync(id);
    }

    public async Task DeleteAsync(Guid id)
    {
        await UnitOfWork.PlatformRepository.DeleteByIdAsync(id);
        await UnitOfWork.SaveChangesAsync();
    }

    public async Task CreateAsync(Platform platform)
    {
        await UnitOfWork.PlatformRepository.InsertAsync(platform);
        await UnitOfWork.SaveChangesAsync();
    }

    public async Task UpdateAsync(Platform platform)
    {
        UnitOfWork.PlatformRepository.Update(platform);
        await UnitOfWork.SaveChangesAsync();
    }

    public async Task<IEnumerable<Platform>> GetByGameKeyAsync(string gameKey)
    {
        var platforms = await UnitOfWork.PlatformRepository.GetByGameKeyAsync(gameKey);
        return platforms;
    }
}