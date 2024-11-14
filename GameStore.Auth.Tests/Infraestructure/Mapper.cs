using AutoMapper;
using GameStore.Auth.Infraestructure;

namespace GameStore.Auth.Tests.Infraestructure;

public static class Mapper
{
    internal static IMapper Create()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<AutoMapperProfile>();
        });

        return config.CreateMapper();
    }
}