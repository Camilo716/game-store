using AutoMapper;
using GameStore.Auth.Core.Models;
using GameStore.Auth.Infraestructure.Entities;

namespace GameStore.Auth.Infraestructure;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Role, RoleModel>();

        CreateMap<Privilege, PrivilegeModel>();
    }
}