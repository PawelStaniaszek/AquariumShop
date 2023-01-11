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
                .ForMember(a => a.Id, b => b.MapFrom(t => t.Id))
                .ForMember(a => a.Price, b => b.MapFrom(t => t.Price))
                .ForMember(a => a.Description, b => b.MapFrom(t => t.Description))
                .ForMember(a => a.Name, b => b.MapFrom(t => t.Name))
                .ForMember(a => a.LongDescription, b => b.MapFrom(t => t.LongDescription))
                .ForMember(a => a.Picture, b => b.MapFrom(t => t.Picture))
                .ForMember(a => a.Category, b => b.MapFrom(t => t.Category.Name));

            CreateMap<ApiUser, UserDTO>().ReverseMap();
            CreateMap<Category, CategoryDto>();
        }
    }
}
