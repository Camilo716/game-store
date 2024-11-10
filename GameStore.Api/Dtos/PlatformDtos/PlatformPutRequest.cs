namespace GameStore.Api.Dtos.PlatformDtos;

public class PlatformPutRequest
{
    public SimplePlatformWithIdDto Platform { get; set; }

    public bool IsValid()
    {
        return Platform is not null
            && Platform.Id != Guid.Empty
            && !string.IsNullOrWhiteSpace(Platform.Type);
    }
}