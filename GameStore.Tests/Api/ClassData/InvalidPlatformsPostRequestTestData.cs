using System.Collections;
using GameStore.Api.Dtos.PlatformDtos;
using GameStore.Tests.Seed;

namespace GameStore.Tests.Api.ClassData;

public class InvalidPlatformsPostRequestTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return
        [
            new PlatformPostRequest() { Platform = null }
        ];

        SimplePlatformDto invalidPlatformMissingType = GetValidPlatformDto();
        invalidPlatformMissingType.Type = null;
        yield return
        [
            new PlatformPostRequest() { Platform = invalidPlatformMissingType }
        ];
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private static SimplePlatformDto GetValidPlatformDto()
    {
        return new SimplePlatformDto()
        {
            Type = PlatformSeed.Mobile.Type,
        };
    }
}