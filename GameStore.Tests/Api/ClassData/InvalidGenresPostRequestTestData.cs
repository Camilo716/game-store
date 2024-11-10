using System.Collections;
using GameStore.Api.Dtos.GenreDtos;
using GameStore.Tests.Seed;

namespace GameStore.Tests.Api.ClassData;

public class InvalidGenresPostRequestTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return
        [
            new GenrePostRequest() { Genre = null }
        ];

        SimpleGenreDto invalidGenreMissingName = GetValidGenreDto();
        invalidGenreMissingName.Name = null;
        yield return
        [
            new GenrePostRequest() { Genre = invalidGenreMissingName }
        ];
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private static SimpleGenreDto GetValidGenreDto()
    {
        return new()
        {
            Name = GenreSeed.Action.Name,
            ParentGenreId = GenreSeed.Action.ParentGenreId,
        };
    }
}