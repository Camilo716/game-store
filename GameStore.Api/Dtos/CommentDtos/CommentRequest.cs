using System.Text.Json.Serialization;

namespace GameStore.Api.Dtos.CommentDtos;

[method: JsonConstructor]
public record CommentRequest(
    SimpleComment Comment,
    Guid? ParentId);