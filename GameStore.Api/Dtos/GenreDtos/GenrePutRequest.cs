namespace GameStore.Api.Dtos.GenreDtos;

public class GenrePutRequest
{
    public SimpleGenreWithIdDto Genre { get; set; }

    public bool IsValid()
    {
        return Genre is not null
            && Genre.Id != Guid.Empty
            && !string.IsNullOrWhiteSpace(Genre.Name);
    }
}