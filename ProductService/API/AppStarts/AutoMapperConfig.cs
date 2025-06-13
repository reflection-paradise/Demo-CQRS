

using Application.Command;
using AutoMapper;
using Domain.Entities;

namespace API.AppStarts
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Product, CreateProductDTO>().ReverseMap();
            CreateMap<Product, UpdateProductDTO>().ReverseMap();
        }
    }
}       
