using AutoMapper;
using GameStore.Auth.Core.Privilege;
using GameStore.Auth.Core.Role;
using GameStore.Auth.Core.User;
using GameStore.Auth.Infraestructure.Entities;

namespace GameStore.Auth.Infraestructure;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Role, RoleModel>()
            .ReverseMap();

        CreateMap<Privilege, PrivilegeModel>()
            .ReverseMap();

        CreateMap<User, UserModel>()
            .ReverseMap();
    }
}