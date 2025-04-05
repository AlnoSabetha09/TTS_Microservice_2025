using System;
using AutoMapper;
using ProductCatalog.DTOs;
using ProductCatalog.Entities;

namespace ProductCatalog.RequestHelpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductsDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName));
        CreateMap<CreateProductDto, Product>();
    }
}
