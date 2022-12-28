using AutoMapper;
using AquariumShop.Dtos;
using Domain.Models;

namespace AquariumShop.Mappings
{
    public class AutoMappers :Profile
    {
        public AutoMappers()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(a => a.Cena, b => b.MapFrom(t => t.Price))
                .ForMember(a => a.Opis, b => b.MapFrom(t => t.Description))
                .ForMember(a => a.Nazwa, b => b.MapFrom(t => t.Name))
                .ForMember(a => a.Opis_dlugi, b => b.MapFrom(t => t.LongDescription))
                .ForMember(a => a.obrazek, b => b.MapFrom(t => t.Picture));

            CreateMap<ApiUser, UserDTO>().ReverseMap();
            CreateMap<Category, CategoryDto>();
        }
    }
}
