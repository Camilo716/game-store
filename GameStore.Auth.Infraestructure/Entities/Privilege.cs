namespace GameStore.Auth.Infraestructure.Entities;

public class Privilege
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Key { get; set; }

    public List<Role> Roles { get; set; } =
    [
    ];
}