using GameStore.Auth.Core.User;
using GameStore.Auth.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Auth.Infraestructure.Repositories;

public class UserRepository(
    GameStoreAuthDbContext dbContext)
    : IUserRepository
{
    public async Task BanUserByUserNameAsync(DateTime expirationDate, string userName)
    {
        var user = (await dbContext.Users.FirstOrDefaultAsync(u => u.UserName == userName))
            ?? throw new InvalidOperationException($"User {userName} not found.");

        user.BanExpirationDate = expirationDate;

        dbContext.Entry(user).State = EntityState.Modified;
    }
}