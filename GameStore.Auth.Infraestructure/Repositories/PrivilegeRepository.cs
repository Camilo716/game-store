using GameStore.Auth.Core.Interfaces;
using GameStore.Auth.Core.Models;
using GameStore.Auth.Infraestructure.Data;

namespace GameStore.Auth.Infraestructure.Repositories;

public class PrivilegeRepository(GameStoreAuthDbContext dbContext)
    : BaseRepository<PrivilegeModel>(dbContext),
    IPrivilegeRepository
{
}