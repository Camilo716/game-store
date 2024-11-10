using GameStore.Payment.Core.GameClient;

namespace GameStore.Payment.Tests.Api.Mocks;

public class MockGameServiceClient(HttpClient httpClient) : IGameServiceClient
{
#pragma warning disable IDE0052 // Remove unread private members
    private readonly HttpClient _httpClient = httpClient;
#pragma warning restore IDE0052 // Remove unread private members

    public Task<Game> GetByKeyAsync(string key)
    {
        return Task.FromResult(new Game()
        {
            Id = Guid.Parse("0a2bd33d-030a-4502-9806-c2fdd1b2c4fb"),
            Name = "Days Gone",
            Key = "daysGone",
            Description = "Description",
            Price = 10.0,
            UnitsInStock = 2,
            Discount = 0,
        });
    }
}