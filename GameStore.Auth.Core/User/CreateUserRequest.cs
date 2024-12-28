namespace GameStore.Auth.Core.User;

public class CreateUserRequest
{
    public UserModel User { get; set; }

    public IEnumerable<string> Roles { get; set; }

    public string Password { get; set; }
}