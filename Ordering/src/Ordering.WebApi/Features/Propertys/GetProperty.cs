namespace Ordering.WebApi.Features.Propertys
{
    using Ordering.Core.Dtos.Property;
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

    public static class GetProperty
    {
        public class PropertyQuery : IRequest<PropertyDto>
        {
            public Guid PropertyId { get; set; }

            public PropertyQuery(Guid propertyId)
            {
                PropertyId = propertyId;
            }
        }

        public class Handler : IRequestHandler<PropertyQuery, PropertyDto>
        {
            private readonly OrderingDbContext _db;
            private readonly IMapper _mapper;

            public Handler(OrderingDbContext db, IMapper mapper)
            {
                _mapper = mapper;
                _db = db;
            }

            public async Task<PropertyDto> Handle(PropertyQuery request, CancellationToken cancellationToken)
            {
                // add logger (and a try catch with logger so i can cap the unexpected info)........ unless this happens in my logger decorator that i am going to add?

                var result = await _db.Propertys
                    .ProjectTo<PropertyDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(p => p.PropertyId == request.PropertyId);

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