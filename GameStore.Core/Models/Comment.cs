namespace GameStore.Core.Models;

public class Comment
{
    public Guid Id { get; set; }

    public string Body { get; set; }

    public Guid GameId { get; set; }

    public Game Game { get; set; }

    public Guid ParentCommentId { get; set; }

    public IEnumerable<Comment> ChildrenComments { get; set; }
}