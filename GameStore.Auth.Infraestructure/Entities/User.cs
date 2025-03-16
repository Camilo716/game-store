using Microsoft.AspNetCore.Identity;

namespace GameStore.Auth.Infraestructure.Entities;

public class User : IdentityUser
{
    public DateTime? BanExpirationDate { get; set; }
}