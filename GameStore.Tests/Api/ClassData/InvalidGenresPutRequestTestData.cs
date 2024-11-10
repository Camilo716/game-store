using System.Collections;
using GameStore.Api.Dtos.GenreDtos;
using GameStore.Tests.Seed;

namespace GameStore.Tests.Api.ClassData;

public class InvalidGenresPutRequestTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return
        [
            new GenrePutRequest() { Genre = null }
        ];

        SimpleGenreWithIdDto invalidGenreMissingId = GetValidGenreDto();
        invalidGenreMissingId.Id = Guid.Empty;
        yield return
        [
            new GenrePutRequest() { Genre = invalidGenreMissingId }
        ];

        SimpleGenreWithIdDto invalidGenreMissingName = GetValidGenreDto();
        invalidGenreMissingName.Name = null;
        yield return
        [
            new GenrePutRequest() { Genre = invalidGenreMissingName }
        ];
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private static SimpleGenreWithIdDto GetValidGenreDto()
    {
        return new()
        {
            Id = GenreSeed.Action.Id,
            Name = GenreSeed.Action.Name,
            ParentGenreId = GenreSeed.Action.ParentGenreId,
        };
    }
}