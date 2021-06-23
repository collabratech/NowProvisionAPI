namespace Ordering.WebApi.Features.Offices
{
    using Ordering.Core.Dtos.Office;
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

    public static class GetOffice
    {
        public class OfficeQuery : IRequest<OfficeDto>
        {
            public Guid OfficeId { get; set; }

            public OfficeQuery(Guid officeId)
            {
                OfficeId = officeId;
            }
        }

        public class Handler : IRequestHandler<OfficeQuery, OfficeDto>
        {
            private readonly OrderingDbContext _db;
            private readonly IMapper _mapper;

            public Handler(OrderingDbContext db, IMapper mapper)
            {
                _mapper = mapper;
                _db = db;
            }

            public async Task<OfficeDto> Handle(OfficeQuery request, CancellationToken cancellationToken)
            {
                // add logger (and a try catch with logger so i can cap the unexpected info)........ unless this happens in my logger decorator that i am going to add?

                var result = await _db.Offices
                    .ProjectTo<OfficeDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(o => o.OfficeId == request.OfficeId);

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