using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Customer.svc.DTOs;

namespace Customer.svc.RequestHelpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer.svc.Entities.Customer, CustomerDto>();
            CreateMap<CreateCustomerDto, Customer.svc.Entities.Customer>();
        }
    }
}