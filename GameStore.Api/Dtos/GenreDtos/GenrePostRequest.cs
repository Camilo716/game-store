namespace GameStore.Api.Dtos.GenreDtos;

public class GenrePostRequest
{
    public SimpleGenreDto Genre { get; set; }

    public bool IsValid()
    {
        return Genre is not null && !string.IsNullOrWhiteSpace(Genre.Name);
    }
}