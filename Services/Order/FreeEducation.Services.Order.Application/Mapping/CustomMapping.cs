using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeEducation.Services.Order.Application.Mapping
{
    public class CustomMapping :Profile
    {
        public CustomMapping()
        {
            CreateMap<Domain.OrderAggregate.Order, Dtos.OrderDto>().ReverseMap();
            CreateMap<Domain.OrderAggregate.OrderItem, Dtos.OrderItemDto>().ReverseMap();
            CreateMap<Domain.OrderAggregate.Address, Dtos.AddressDto>().ReverseMap();
        }
    }
}
