using GameStore.Core.Comment;
using GameStore.Core.Comment.Formatter;

namespace GameStore.Tests.Core;

public class CommentFormatterTest
{
    [Fact]
    public void Format_GivenSimpleComment_DoNotApplyFormatting()
    {
        Comment comment = new()
        {
            Body = "My Body",
            Type = CommentType.Comment,
        };
        var formatter = new CommentFormatter();

        CommentResponse formattedComment = formatter.Format(comment);

        Assert.Equal("My Body", formattedComment.FormattedBody);
    }

    [Fact]
    public void Format_GivenReplyComment_FormatsCorrectly()
    {
        Comment comment = new()
        {
            Body = "My Reply",
            Type = CommentType.Reply,
            ParentComment = new Comment()
            {
                Body = "My Body",
                UserName = "UserName",
            },
        };
        var formatter = new CommentFormatter();

        CommentResponse formattedComment = formatter.Format(comment);

        Assert.Equal("[UserName], My Reply", formattedComment.FormattedBody);
    }

    [Fact]
    public void Format_GivenQuoteComment_FormatsCorrectly()
    {
        Comment comment = new()
        {
            Body = "My Quote",
            Type = CommentType.Quote,
            ParentComment = new Comment()
            {
                Body = "My Body",
            },
        };
        var formatter = new CommentFormatter();

        CommentResponse formattedComment = formatter.Format(comment);

        Assert.Equal("[My Body], My Quote", formattedComment.FormattedBody);
    }

    [Fact]
    public void Format_GivenSimpleDeletedComment_ReturnsDefaultText()
    {
        Comment comment = new()
        {
            Body = "My Body",
            Type = CommentType.Comment,
            Deleted = true,
        };
        var formatter = new CommentFormatter();

        CommentResponse formattedComment = formatter.Format(comment);

        Assert.Equal("A comment/quote was deleted", formattedComment.FormattedBody);
    }

    [Fact]
    public void Format_GivenQuoteDeletedComment_HidesDeletedQuote()
    {
        Comment comment = new()
        {
            Body = "My Quote",
            Type = CommentType.Quote,
            ParentComment = new Comment()
            {
                Body = "My Body",
                Deleted = true,
            },
        };
        var formatter = new CommentFormatter();

        CommentResponse formattedComment = formatter.Format(comment);

        Assert.Equal("[A comment/quote was deleted], My Quote", formattedComment.FormattedBody);
    }
}