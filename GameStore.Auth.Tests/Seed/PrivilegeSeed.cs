using GameStore.Auth.Core.Models;
using GameStore.Auth.Infraestructure.Entities;

namespace GameStore.Auth.Tests.Seed;

public static class PrivilegeSeed
{
    public static Privilege AddGame => new()
    {
        Id = Guid.Parse("2b35db59-2aea-458b-9841-7eb3de203ab5"),
        Key = "AddGame",
    };

    public static Privilege ViewGame => new()
    {
        Id = Guid.Parse("dcabd895-44d4-49c4-ac3b-c274840b92ee"),
        Key = "ViewGame",
    };

    public static List<Privilege> GetPrivileges()
    {
        return
        [
            AddGame,
            ViewGame
        ];
    }

    public static List<PrivilegeModel> GetPrivilegeModels()
    {
        var mapper = Infraestructure.Mapper.Create();
        return mapper.Map<IEnumerable<PrivilegeModel>>(GetPrivileges()).ToList();
    }
}