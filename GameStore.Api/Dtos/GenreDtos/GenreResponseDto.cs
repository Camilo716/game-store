using GameStore.Core.Models;

namespace GameStore.Api.Dtos.GenreDtos;

public class GenreResponseDto(Genre genre)
{
    public Guid Id => Genre.Id;

    public string Name => Genre.Name;

    public Guid? ParentGenreId => Genre.ParentGenreId;

    private Genre Genre => genre;
}