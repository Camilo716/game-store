namespace GameStore.Core.Dtos;

public class GameFile
{
    public string FileName { get; set; }

    public Stream Content { get; set; }

    public string ContentType { get; set; }
}