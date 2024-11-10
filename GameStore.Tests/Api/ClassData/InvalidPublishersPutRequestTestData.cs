using System.Collections;
using GameStore.Api.Dtos.PublisherDtos;
using GameStore.Tests.Seed;

namespace GameStore.Tests.Api.ClassData;

public class InvalidPublishersPutRequestTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return
        [
            new PublisherPutRequest() { Publisher = null }
        ];

        SimplePublisherWithIdDto invalidPublisherMissingCompanyName = GetValidPublisherDto();
        invalidPublisherMissingCompanyName.CompanyName = null;
        yield return
        [
            new PublisherPutRequest() { Publisher = invalidPublisherMissingCompanyName }
        ];

        SimplePublisherWithIdDto invalidPublisherMissingId = GetValidPublisherDto();
        invalidPublisherMissingId.Id = Guid.Empty;
        yield return
        [
            new PublisherPutRequest() { Publisher = invalidPublisherMissingId }
        ];
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private static SimplePublisherWithIdDto GetValidPublisherDto()
    {
        return new SimplePublisherWithIdDto()
        {
            Id = PublisherSeed.Activision.Id,
            CompanyName = PublisherSeed.Activision.CompanyName,
        };
    }
}