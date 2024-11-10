using GameStore.Core.Interfaces;
using GameStore.Core.Models;

namespace GameStore.Core.Services;

public class GenreService(IUnitOfWork unitOfWork)
    : IGenreService
{
    private IUnitOfWork UnitOfWork => unitOfWork;

    public async Task<Genre> GetByIdAsync(Guid id)
    {
        return await UnitOfWork.GenreRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Genre>> GetAllAsync()
    {
        return await UnitOfWork.GenreRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Genre>> GetByParentIdAsync(Guid parentId)
    {
        return await UnitOfWork.GenreRepository.GetByParentIdAsync(parentId);
    }

    public async Task DeleteAsync(Guid id)
    {
        await UnitOfWork.GenreRepository.DeleteByIdAsync(id);
        await UnitOfWork.SaveChangesAsync();
    }

    public async Task CreateAsync(Genre genre)
    {
        await UnitOfWork.GenreRepository.InsertAsync(genre);
        await UnitOfWork.SaveChangesAsync();
    }

    public async Task UpdateAsync(Genre genre)
    {
        UnitOfWork.GenreRepository.Update(genre);
        await UnitOfWork.SaveChangesAsync();
    }

    public async Task<IEnumerable<Genre>> GetByGameKeyAsync(string gameKey)
    {
        var genres = await UnitOfWork.GenreRepository.GetByGameKeyAsync(gameKey);
        return genres;
    }
}