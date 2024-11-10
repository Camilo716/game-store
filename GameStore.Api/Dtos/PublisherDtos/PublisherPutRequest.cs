namespace GameStore.Api.Dtos.PublisherDtos;

public class PublisherPutRequest
{
    public SimplePublisherWithIdDto Publisher { get; set; }

    public bool IsValid()
    {
        return Publisher is not null
            && Publisher.Id != Guid.Empty
            && !string.IsNullOrWhiteSpace(Publisher.CompanyName);
    }
}