using AutoMapper;
using Business.Models;
using Data.Entities;

namespace Business
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Game, GameModel>()
                .ForMember(gm => gm.Genres, g => g.MapFrom(x => x.Genres))
                .ReverseMap();

            CreateMap<GameGenre, GameGenreModel>()
                .ForMember(gm => gm.GameIds, g => g.MapFrom(x => x.Games.Select(g => g.Id).ToList())) // Добавьте эту настройку
                .ReverseMap();

            CreateMap<Order, OrderModel>()
                .ReverseMap();
        }
    }
}
