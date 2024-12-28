using GameStore.Core.Publisher;

namespace GameStore.Api.Dtos.PublisherDtos;

public class PublisherResponseDto(Publisher publisher)
{
    public Guid Id => Publisher.Id;

    public string CompanyName => Publisher.CompanyName;

    public string HomePage => Publisher.HomePage;

    public string Description => Publisher.Description;

    private Publisher Publisher => publisher;
}