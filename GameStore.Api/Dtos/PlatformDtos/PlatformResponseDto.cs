using GameStore.Core.Models;

namespace GameStore.Api.Dtos.PlatformDtos;

public class PlatformResponseDto(Platform platform)
{
    public Guid Id => Platform.Id;

    public string Type => Platform.Type;

    private Platform Platform => platform;
}