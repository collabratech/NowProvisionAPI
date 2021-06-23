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

    public static class UpdateOrder
    {
        public class UpdateOrderCommand : IRequest<bool>
        {
            public Guid Id { get; set; }
            public OrderForUpdateDto OrderToUpdate { get; set; }

            public UpdateOrderCommand(Guid order, OrderForUpdateDto orderToUpdate)
            {
                Id = order;
                OrderToUpdate = orderToUpdate;
            }
        }

        public class CustomUpdateOrderValidation : OrderForManipulationDtoValidator<OrderForUpdateDto>
        {
            public CustomUpdateOrderValidation()
            {
            }
        }

        public class Handler : IRequestHandler<UpdateOrderCommand, bool>
        {
            private readonly OrderingDbContext _db;
            private readonly IMapper _mapper;

            public Handler(OrderingDbContext db, IMapper mapper)
            {
                _mapper = mapper;
                _db = db;
            }

            public async Task<bool> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
            {
                // add logger or use decorator

                var recordToUpdate = await _db.Orders
                    .FirstOrDefaultAsync(o => o.Id == request.Id);

                if (recordToUpdate == null)
                {
                    // log error
                    throw new KeyNotFoundException();
                }

                _mapper.Map(request.OrderToUpdate, recordToUpdate);
                var saveSuccessful = await _db.SaveChangesAsync() > 0;

                if (!saveSuccessful)
                {
                    // add log
                    throw new Exception("Unable to save the requested changes. Please check the logs for more information.");
                }

                return true;
            }
        }
    }
}