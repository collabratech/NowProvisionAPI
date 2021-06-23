namespace Ordering.WebApi.Features.Orders
{
    using Ordering.Core.Entities;
    using Ordering.Core.Dtos.Order;
    using Ordering.Core.Exceptions;
    using Ordering.Infrastructure.Contexts;
    using Ordering.WebApi.Features.Orders.Validators;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public static class AddOrder
    {
        public class AddOrderCommand : IRequest<OrderDto>
        {
            public OrderForCreationDto OrderToAdd { get; set; }

            public AddOrderCommand(OrderForCreationDto orderToAdd)
            {
                OrderToAdd = orderToAdd;
            }
        }

        public class CustomCreateOrderValidation : OrderForManipulationDtoValidator<OrderForCreationDto>
        {
            public CustomCreateOrderValidation()
            {
            }
        }

        public class Handler : IRequestHandler<AddOrderCommand, OrderDto>
        {
            private readonly OrderingDbContext _db;
            private readonly IMapper _mapper;

            public Handler(OrderingDbContext db, IMapper mapper)
            {
                _mapper = mapper;
                _db = db;
            }

            public async Task<OrderDto> Handle(AddOrderCommand request, CancellationToken cancellationToken)
            {
                if (await _db.Orders.AnyAsync(o => o.Id == request.OrderToAdd.Id))
                {
                    throw new ConflictException("Order already exists with this primary key.");
                }

                var order = _mapper.Map<Order> (request.OrderToAdd);
                _db.Orders.Add(order);
                var saveSuccessful = await _db.SaveChangesAsync() > 0;

                if (saveSuccessful)
                {
                    return await _db.Orders
                        .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync(o => o.Id == order.Id);
                }
                else
                {
                    // add log
                    throw new Exception("Unable to save the new record. Please check the logs for more information.");
                }
            }
        }
    }
}