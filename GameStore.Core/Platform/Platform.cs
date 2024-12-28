namespace GameStore.Core.Platform;

public class Platform
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Type { get; set; }

    public List<Game.Game> Games { get; set; }
}