using GameStore.Core.Interfaces;
using GameStore.Core.Services;
using GameStore.Tests.Seed;
using Moq;

namespace GameStore.Tests.Core;

public class CommentServiceTest
{
    [Fact]
    public async Task GetByGameKey_GivenValidKey_ReturnsComments()
    {
        Mock<IUnitOfWork> unitOfWork = GetDummyUnitOfWorkMock();
        var commentService = new CommentService(unitOfWork.Object);
        string gameKey = GameSeed.GearsOfWar.Key;

        var comments = await commentService.GetByGameKeyAsync(gameKey);

        Assert.NotNull(comments);
    }

    private static Mock<IUnitOfWork> GetDummyUnitOfWorkMock()
    {
        Mock<IUnitOfWork> unitOfWork = new();

        unitOfWork.Setup(m => m.CommentRepository.GetByGameKeyAsync(It.IsAny<string>()))
            .ReturnsAsync([CommentSeed.PositiveComment]);

        return unitOfWork;
    }
}