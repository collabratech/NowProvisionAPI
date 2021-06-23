namespace Ordering.WebApi.Features.Orders
{
    using Ordering.Core.Dtos.Order;
    using Ordering.Core.Exceptions;
    using Ordering.Infrastructure.Contexts;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public static class GetOrder
    {
        public class OrderQuery : IRequest<OrderDto>
        {
            public Guid Id { get; set; }

            public OrderQuery(Guid id)
            {
                Id = id;
            }
        }

        public class Handler : IRequestHandler<OrderQuery, OrderDto>
        {
            private readonly OrderingDbContext _db;
            private readonly IMapper _mapper;

            public Handler(OrderingDbContext db, IMapper mapper)
            {
                _mapper = mapper;
                _db = db;
            }

            public async Task<OrderDto> Handle(OrderQuery request, CancellationToken cancellationToken)
            {
                // add logger (and a try catch with logger so i can cap the unexpected info)........ unless this happens in my logger decorator that i am going to add?

                var result = await _db.Orders
                    .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(o => o.Id == request.Id);

                if (result == null)
                {
                    // log error
                    throw new KeyNotFoundException();
                }

                return result;
            }
        }
    }
}