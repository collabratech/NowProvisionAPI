namespace NowProvisionAPI.WebApi.Features.NowProvs
{
    using NowProvisionAPI.Core.Entities;
    using NowProvisionAPI.Core.Dtos.NowProv;
    using NowProvisionAPI.Core.Exceptions;
    using NowProvisionAPI.Infrastructure.Contexts;
    using NowProvisionAPI.Core.Wrappers;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Sieve.Models;
    using Sieve.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public static class GetNowProvList
    {
        public class NowProvListQuery : IRequest<PagedList<NowProvDto>>
        {
            public NowProvParametersDto QueryParameters { get; set; }

            public NowProvListQuery(NowProvParametersDto queryParameters)
            {
                QueryParameters = queryParameters;
            }
        }

        public class Handler : IRequestHandler<NowProvListQuery, PagedList<NowProvDto>>
        {
            private readonly NowProvisionApiDbContext _db;
            private readonly SieveProcessor _sieveProcessor;
            private readonly IMapper _mapper;

            public Handler(NowProvisionApiDbContext db, IMapper mapper, SieveProcessor sieveProcessor)
            {
                _mapper = mapper;
                _db = db;
                _sieveProcessor = sieveProcessor;
            }

            public async Task<PagedList<NowProvDto>> Handle(NowProvListQuery request, CancellationToken cancellationToken)
            {
                if (request.QueryParameters == null)
                {
                    // log error
                    throw new ApiException("Invalid query parameters.");
                }

                var collection = _db.NowProvs
                    as IQueryable<NowProv>;

                var sieveModel = new SieveModel
                {
                    Sorts = request.QueryParameters.SortOrder ?? "Id",
                    Filters = request.QueryParameters.Filters
                };

                var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
                var dtoCollection = appliedCollection
                    .ProjectTo<NowProvDto>(_mapper.ConfigurationProvider);

                return await PagedList<NowProvDto>.CreateAsync(dtoCollection,
                    request.QueryParameters.PageNumber,
                    request.QueryParameters.PageSize);
            }
        }
    }
}