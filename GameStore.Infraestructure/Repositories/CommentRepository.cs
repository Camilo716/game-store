using GameStore.Core.Interfaces;
using GameStore.Core.Models;
using GameStore.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Infraestructure.Repositories;

public class CommentRepository(
    GameStoreDbContext dbContext)
    : ICommentRepository
{
    public async Task<IEnumerable<Comment>> GetByGameKeyAsync(string key)
    {
        var comments = await dbContext.Comments
            .Where(c => c.Game.Key == key)
            .ToListAsync();

        ILookup<Guid?, Comment> commentLookup = comments.ToLookup(c => c.ParentCommentId);

        foreach (var comment in comments)
        {
            comment.ChildrenComments = commentLookup[comment.Id].ToList();
        }

        Func<Comment, bool> isRootLevel = c => c.ParentCommentId == Guid.Empty || c.ParentCommentId is null;
        return comments.Where(isRootLevel);
    }

    public async Task InsertAsync(Comment comment)
    {
        await dbContext.Comments.AddAsync(comment);
    }
}