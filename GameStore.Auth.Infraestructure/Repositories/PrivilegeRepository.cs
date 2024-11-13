using AutoMapper;
using GameStore.Auth.Core.Interfaces;
using GameStore.Auth.Core.Models;
using GameStore.Auth.Infraestructure.Data;
using GameStore.Auth.Infraestructure.Entities;

namespace GameStore.Auth.Infraestructure.Repositories;

public class PrivilegeRepository(GameStoreAuthDbContext dbContext, IMapper mapper)
    : BaseRepository<Privilege>(dbContext),
    IPrivilegeRepository
{
    public IMapper Mapper => mapper;

    async Task<IEnumerable<PrivilegeModel>> IPrivilegeRepository.GetAllAsync()
    {
        var privileges = await GetAllAsync();
        return mapper.Map<IEnumerable<PrivilegeModel>>(privileges);
    }
}