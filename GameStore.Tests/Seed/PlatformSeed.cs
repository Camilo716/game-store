using GameStore.Core.Models;

namespace GameStore.Tests.Seed;

public static class PlatformSeed
{
    public static Platform Mobile => new() { Id = Guid.Parse("89f83d50-34c7-4a3d-8dfc-77d5b89a39ab"), Type = "Mobile" };

    public static Platform Browser => new() { Id = Guid.Parse("5e5c5ce6-ba25-4d2e-924c-ed14735dc2f8"), Type = "Browser" };

    public static Platform Desktop => new() { Id = Guid.Parse("7455cf5c-4f7d-4f9c-8bbe-f6da2fea98eb"), Type = "Desktop" };

    public static Platform Console => new() { Id = Guid.Parse("3d88e672-4b90-45e0-be2b-202673459273"), Type = "Console" };

    public static List<Platform> GetPlatforms()
    {
        return
        [
            Mobile,
            Browser,
            Desktop,
            Console
        ];
    }
}