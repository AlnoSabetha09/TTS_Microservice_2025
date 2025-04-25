using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Transaction.svc.DTOs;
using Transaction.svc.Entities;

namespace Transaction.svc.RequestHelpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateTransactionDto, Sale>()
                .ForMember(dest => dest.SaleItems, opt => opt.MapFrom(src => src.Items));

            CreateMap<CreateSaleItemDto, SaleItem>();

            CreateMap<Sale, TransactionDto>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.SaleItems))
                .ForMember(dest => dest.CustomerName, opt => opt.Ignore());

            CreateMap<SaleItem, TransactionItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.Ignore());

            CreateMap<CustomerDto, CustomerDto>();
            CreateMap<ProductDto, ProductDto>();
        }
    }
}