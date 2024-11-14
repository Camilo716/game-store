using GameStore.Auth.Core.Models;

namespace GameStore.Auth.Core.Dtos;

public class CreateUserRequest
{
    public UserModel User { get; set; }

    public IEnumerable<string> Roles { get; set; }

    public string Password { get; set; }
}