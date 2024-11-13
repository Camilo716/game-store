using GameStore.Auth.Core.Models;

namespace GameStore.Auth.Tests.Seed;

public static class PrivilegeSeed
{
    public static PrivilegeModel AddGame => new()
    {
        Id = Guid.Parse("2b35db59-2aea-458b-9841-7eb3de203ab5"),
        Key = "AddGame",
    };

    public static PrivilegeModel ViewGame => new()
    {
        Id = Guid.Parse("dcabd895-44d4-49c4-ac3b-c274840b92ee"),
        Key = "ViewGame",
    };

    public static List<PrivilegeModel> GetPrivileges()
    {
        return
        [
            AddGame,
            ViewGame
        ];
    }
}