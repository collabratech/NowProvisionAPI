namespace NowProvisionAPI.WebApi.Features.NowProvs
{
    using NowProvisionAPI.Core.Dtos.NowProv;
    using NowProvisionAPI.Core.Exceptions;
    using NowProvisionAPI.Infrastructure.Contexts;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public static class GetNowProv
    {
        public class NowProvQuery : IRequest<NowProvDto>
        {
            public Guid Id { get; set; }

            public NowProvQuery(Guid id)
            {
                Id = id;
            }
        }

        public class Handler : IRequestHandler<NowProvQuery, NowProvDto>
        {
            private readonly NowProvisionApiDbContext _db;
            private readonly IMapper _mapper;

            public Handler(NowProvisionApiDbContext db, IMapper mapper)
            {
                _mapper = mapper;
                _db = db;
            }

            public async Task<NowProvDto> Handle(NowProvQuery request, CancellationToken cancellationToken)
            {
                // add logger (and a try catch with logger so i can cap the unexpected info)........ unless this happens in my logger decorator that i am going to add?

                var result = await _db.NowProvs
                    .ProjectTo<NowProvDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(n => n.Id == request.Id);

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