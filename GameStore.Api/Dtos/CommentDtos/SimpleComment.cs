using System.Text.Json.Serialization;
using GameStore.Core.Comment;

namespace GameStore.Api.Dtos.CommentDtos;

[method: JsonConstructor]
public record SimpleComment(
    string UserName,
    string Body,
    CommentType Type);