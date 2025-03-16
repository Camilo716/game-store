using GameStore.Core.Publisher;

namespace GameStore.Tests.Seed;

public static class PublisherSeed
{
    public static Publisher Activision => new()
    {
        Id = Guid.Parse("aaa95299-994f-4c5e-ac8a-e63de303b49b"),
        CompanyName = "Activision",
        HomePage = "fakeactivision.org",
        Description = "Description",
    };

    public static List<Publisher> GetPublishers()
    {
        return
        [
            Activision,
        ];
    }
}