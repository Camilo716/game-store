using AutoMapper;
using GameStore.Api.Dtos.GameDtos;
using GameStore.Core.Models;

namespace GameStore.Api;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<GamePostRequest, Game>()
            .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Game.Key))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Game.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Game.Description))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Game.Price))
            .ForMember(dest => dest.UnitsInStock, opt => opt.MapFrom(src => src.Game.UnitsInStock))
            .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Game.Discount))
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres.Select(g => new Genre() { Id = g })))
            .ForMember(dest => dest.Platforms, opt => opt.MapFrom(src => src.Platforms.Select(p => new Platform() { Id = p })))
            .ForMember(dest => dest.PublisherId, opt => opt.MapFrom(src => src.Publisher))
            .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => new Publisher() { Id = src.Publisher.GetValueOrDefault() }));

        CreateMap<GamePutRequest, Game>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Game.Id))
            .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Game.Key))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Game.Name))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Game.Price))
            .ForMember(dest => dest.UnitsInStock, opt => opt.MapFrom(src => src.Game.UnitsInStock))
            .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Game.Discount))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Game.Description))
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres.Select(g => new Genre() { Id = g })))
            .ForMember(dest => dest.Platforms, opt => opt.MapFrom(src => src.Platforms.Select(p => new Platform() { Id = p })))
            .ForMember(dest => dest.PublisherId, opt => opt.MapFrom(src => src.Publisher))
            .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => new Publisher() { Id = src.Publisher.GetValueOrDefault() }));
    }
}