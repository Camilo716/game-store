namespace GameStore.Core.Models;

public class Publisher
{
    public Guid Id { get; set; }

    public string CompanyName { get; set; }

    public string? HomePage { get; set; }

    public string? Description { get; set; }

    public List<Game> Games { get; set; }
}