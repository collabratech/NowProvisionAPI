namespace NowProvisionAPI.WebApi.Features.Offices
{
    using NowProvisionAPI.Core.Entities;
    using NowProvisionAPI.Core.Dtos.Office;
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

    public static class GetOfficeList
    {
        public class OfficeListQuery : IRequest<PagedList<OfficeDto>>
        {
            public OfficeParametersDto QueryParameters { get; set; }

            public OfficeListQuery(OfficeParametersDto queryParameters)
            {
                QueryParameters = queryParameters;
            }
        }

        public class Handler : IRequestHandler<OfficeListQuery, PagedList<OfficeDto>>
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

            public async Task<PagedList<OfficeDto>> Handle(OfficeListQuery request, CancellationToken cancellationToken)
            {
                if (request.QueryParameters == null)
                {
                    // log error
                    throw new ApiException("Invalid query parameters.");
                }

                var collection = _db.Offices
                    as IQueryable<Office>;

                var sieveModel = new SieveModel
                {
                    Sorts = request.QueryParameters.SortOrder ?? "OfficeId",
                    Filters = request.QueryParameters.Filters
                };

                var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
                var dtoCollection = appliedCollection
                    .ProjectTo<OfficeDto>(_mapper.ConfigurationProvider);

                return await PagedList<OfficeDto>.CreateAsync(dtoCollection,
                    request.QueryParameters.PageNumber,
                    request.QueryParameters.PageSize);
            }
        }
    }
}