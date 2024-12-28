namespace GameStore.Core.Comment;

public class Comment
{
    public Guid Id { get; set; }

    public string UserName { get; set; }

    public string Body { get; set; }

    public CommentType Type { get; set; } = CommentType.Comment;

    public Guid GameId { get; set; }

    public Game.Game Game { get; set; }

    public Guid? ParentCommentId { get; set; }

    public Comment? ParentComment { get; set; }

    public List<Comment> ChildrenComments { get; set; } =
    [
    ];
}