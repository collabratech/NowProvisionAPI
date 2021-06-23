namespace Ordering.WebApi.Features.Orders.Mappings
{
    using Ordering.Core.Dtos.Order;
    using AutoMapper;
    using Ordering.Core.Entities;

    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            //createmap<to this, from this>
            CreateMap<Order, OrderDto>()
                .ReverseMap();
            CreateMap<OrderForCreationDto, Order>();
            CreateMap<OrderForUpdateDto, Order>()
                .ReverseMap();
        }
    }
}