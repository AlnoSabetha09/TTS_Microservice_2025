using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.svc.Entities;
using Catalog.svc.DTOs;

namespace Catalog.svc.RequestHelpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName));

            CreateMap<Category, ProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryName));

            CreateMap<CreateProductDto, Product>();

            CreateMap<Category, CategoryDto>();

            CreateMap<CreateCategoryDto, Category>();
        }
    }
}