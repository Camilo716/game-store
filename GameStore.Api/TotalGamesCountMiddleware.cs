using GameStore.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api;

public class TotalGamesCountMiddleware(RequestDelegate next)
{
    private RequestDelegate Next => next;

    public async Task InvokeAsync(HttpContext context, GameStoreDbContext dbContext)
    {
        try
        {
            int totalGamesCount = await dbContext.Games.CountAsync();
            context.Response.Headers.Append("x-total-numbers-of-games", totalGamesCount.ToString());
        }
        catch (Exception)
        {
        }

        await Next(context);
    }
}