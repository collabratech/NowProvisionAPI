namespace NowProvisionAPI.WebApi.Features.Agents
{
    using NowProvisionAPI.Core.Entities;
    using NowProvisionAPI.Core.Dtos.Agent;
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

    public static class GetAgentList
    {
        public class AgentListQuery : IRequest<PagedList<AgentDto>>
        {
            public AgentParametersDto QueryParameters { get; set; }

            public AgentListQuery(AgentParametersDto queryParameters)
            {
                QueryParameters = queryParameters;
            }
        }

        public class Handler : IRequestHandler<AgentListQuery, PagedList<AgentDto>>
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

            public async Task<PagedList<AgentDto>> Handle(AgentListQuery request, CancellationToken cancellationToken)
            {
                if (request.QueryParameters == null)
                {
                    // log error
                    throw new ApiException("Invalid query parameters.");
                }

                var collection = _db.Agents
                    as IQueryable<Agent>;

                var sieveModel = new SieveModel
                {
                    Sorts = request.QueryParameters.SortOrder ?? "AgentId",
                    Filters = request.QueryParameters.Filters
                };

                var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
                var dtoCollection = appliedCollection
                    .ProjectTo<AgentDto>(_mapper.ConfigurationProvider);

                return await PagedList<AgentDto>.CreateAsync(dtoCollection,
                    request.QueryParameters.PageNumber,
                    request.QueryParameters.PageSize);
            }
        }
    }
}