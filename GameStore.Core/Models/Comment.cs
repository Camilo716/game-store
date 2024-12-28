using GameStore.Core.Enums;

namespace GameStore.Core.Models;

public class Comment
{
    public Guid Id { get; set; }

    public string UserName { get; set; }

    public string Body { get; set; }

    public Guid GameId { get; set; }

    public Game Game { get; set; }

    public Guid? ParentCommentId { get; set; }

    public CommentType Type { get; set; } = CommentType.Comment;

    public IEnumerable<Comment> ChildrenComments { get; set; } =
    [
    ];
}