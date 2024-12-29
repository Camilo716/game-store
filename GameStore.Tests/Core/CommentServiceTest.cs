using GameStore.Core.Comment;
using GameStore.Core.Comment.Formatter;
using GameStore.Core.Game;
using GameStore.Core.UnitOfWork;
using GameStore.Tests.Seed;
using Moq;

namespace GameStore.Tests.Core;

public class CommentServiceTest
{
    [Fact]
    public async Task GetByGameKey_GivenValidKey_ReturnsComments()
    {
        Mock<IUnitOfWork> unitOfWork = GetDummyUnitOfWorkMock();
        Mock<ICommentFormatter> commentFormatter = GetDummyCommentFormatter();

        var commentService = new CommentService(unitOfWork.Object, commentFormatter.Object);
        string gameKey = GameSeed.GearsOfWar.Key;

        var comments = await commentService.GetByGameKeyAsync(gameKey);

        Assert.NotNull(comments);
    }

    [Fact]
    public async Task CreateComment_GivenValidGame_CreatesComment()
    {
        // Arrange
        Mock<IUnitOfWork> unitOfWork = new();
        Game game = GameSeed.GearsOfWar;
        string gameKey = game.Key;
        Comment validComment = new()
        {
            Body = "Body",
        };
        Comment createdComment = null;

        unitOfWork.Setup(m => m.GameRepository.GetByKeyAsync(gameKey))
            .ReturnsAsync(game);

        unitOfWork.Setup(m => m.CommentRepository.InsertAsync(It.IsAny<Comment>()))
            .Callback<Comment>(c => createdComment = c);

        CommentService commentService = new(unitOfWork.Object, GetDummyCommentFormatter().Object);

        // Act
        await commentService.CreateAsync(validComment, gameKey);

        // Assert
        Assert.Equal(game.Id, createdComment.GameId);
    }

    private static Mock<IUnitOfWork> GetDummyUnitOfWorkMock()
    {
        Mock<IUnitOfWork> unitOfWork = new();

        unitOfWork.Setup(m => m.CommentRepository.GetByGameKeyAsync(It.IsAny<string>()))
            .ReturnsAsync([CommentSeed.PositiveComment]);

        return unitOfWork;
    }

    private static Mock<ICommentFormatter> GetDummyCommentFormatter()
    {
        Mock<ICommentFormatter> commentFormatter = new();

        commentFormatter
            .Setup(c => c.Format(It.IsAny<Comment>()))
            .Returns((Comment comment) => new CommentResponse().Map(comment));

        return commentFormatter;
    }
}