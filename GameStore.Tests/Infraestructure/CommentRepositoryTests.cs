using GameStore.Core.Comment;
using GameStore.Infraestructure.Data;
using GameStore.Tests.Seed;

namespace GameStore.Tests.Infraestructure;

public class CommentRepositoryTests
{
    [Fact]
    public async Task GetByKey_GivenValidKey_ReturnsCommentsInDatabase()
    {
        using var dbContext = UnitTestHelper.GetUnitTestDbContext();
        var unitOfWork = new UnitOfWork(dbContext);
        string key = GameSeed.GearsOfWar.Key;

        var comments = await unitOfWork.CommentRepository.GetByGameKeyAsync(key);

        Assert.NotNull(comments);
        Assert.All(comments, c => Assert.Equal(GameSeed.GearsOfWar.Id, c.GameId));
    }

    [Fact]
    public async Task Insert_GivenValidComment_InsertsCommentInDatabase()
    {
        using var dbContext = UnitTestHelper.GetUnitTestDbContext();
        var unitOfWork = new UnitOfWork(dbContext);
        Comment validComment = new()
        {
            UserName = "UserName",
            Body = "Body",
        };

        await unitOfWork.CommentRepository.InsertAsync(validComment);
        await unitOfWork.SaveChangesAsync();

        Assert.Equal(CommentSeed.GetComments().Count + 1, dbContext.Comments.Count());
    }
}