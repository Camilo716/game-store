namespace GameStore.Auth.Core.User.Login;

public class LoginRequest
{
    public string Login { get; set; }

    public string Password { get; set; }

    public bool InternalAuth { get; set; }
}