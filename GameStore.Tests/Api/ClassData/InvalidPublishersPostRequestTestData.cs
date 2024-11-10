using System.Collections;
using GameStore.Api.Dtos.PublisherDtos;
using GameStore.Tests.Seed;

namespace GameStore.Tests.Api.ClassData;

public class InvalidPublishersPostRequestTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return
        [
            new PublisherPostRequest() { Publisher = null }
        ];

        SimplePublisherDto invalidPublisherMissingCompanyName = GetValidPublisherDto();
        invalidPublisherMissingCompanyName.CompanyName = null;
        yield return
        [
            new PublisherPostRequest() { Publisher = invalidPublisherMissingCompanyName }
        ];
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private static SimplePublisherDto GetValidPublisherDto()
    {
        return new SimplePublisherDto()
        {
            CompanyName = PublisherSeed.Activision.CompanyName,
        };
    }
}