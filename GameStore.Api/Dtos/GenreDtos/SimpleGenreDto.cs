namespace GameStore.Api.Dtos.GenreDtos;

public class SimpleGenreDto
{
    public string Name { get; set; }

    public Guid? ParentGenreId { get; set; }
}