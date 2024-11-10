using System.Collections;
using GameStore.Api.Dtos.PlatformDtos;
using GameStore.Tests.Seed;

namespace GameStore.Tests.Api.ClassData;

public class InvalidPlatformsPutRequestTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return
        [
            new PlatformPutRequest() { Platform = null }
        ];

        SimplePlatformWithIdDto invalidPlatformMissingType = GetValidPlatformDto();
        invalidPlatformMissingType.Type = null;
        yield return
        [
            new PlatformPutRequest() { Platform = invalidPlatformMissingType }
        ];

        SimplePlatformWithIdDto invalidPlatformMissingId = GetValidPlatformDto();
        invalidPlatformMissingId.Id = Guid.Empty;
        yield return
        [
            new PlatformPutRequest() { Platform = invalidPlatformMissingId }
        ];
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private static SimplePlatformWithIdDto GetValidPlatformDto()
    {
        return new SimplePlatformWithIdDto()
        {
            Id = PlatformSeed.Mobile.Id,
            Type = PlatformSeed.Mobile.Type,
        };
    }
}