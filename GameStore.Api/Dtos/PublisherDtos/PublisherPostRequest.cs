namespace GameStore.Api.Dtos.PublisherDtos;

public class PublisherPostRequest
{
    public SimplePublisherDto Publisher { get; set; }

    public bool IsValid()
    {
        return Publisher is not null && !string.IsNullOrWhiteSpace(Publisher.CompanyName);
    }
}