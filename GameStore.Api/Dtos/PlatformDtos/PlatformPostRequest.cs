namespace GameStore.Api.Dtos.PlatformDtos;

public class PlatformPostRequest
{
    public SimplePlatformDto Platform { get; set; }

    public bool IsValid()
    {
        return Platform is not null && !string.IsNullOrWhiteSpace(Platform.Type);
    }
}