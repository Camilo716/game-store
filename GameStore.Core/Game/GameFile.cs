namespace GameStore.Core.Game;

public class GameFile
{
    public string FileName { get; set; }

    public Stream Content { get; set; }

    public string ContentType { get; set; }
}