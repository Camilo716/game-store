namespace GameStore.Api.Dtos.CommentDtos;

public record CommentRequest(
    SimpleComment Comment,
    Guid? ParentId);