namespace GameStore.Auth.Core.Dtos;

public class LoginRequest
{
    public string Login { get; set; }

    public string Password { get; set; }

    public bool InternalAuth { get; set; }
}