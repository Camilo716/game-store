using GameStore.Auth.Infraestructure.Entities;

namespace GameStore.Auth.Tests.Seed;

public static class RoleSeed
{
    public static Role Role => new()
    {
        Id = "51366600-d6df-4325-b438-999680fecc69",
        Name = "Admin",
        NormalizedName = "Admin",
        Privileges =
        [
            PrivilegeSeed.AddGame,
            PrivilegeSeed.ViewGame
        ],
    };
}