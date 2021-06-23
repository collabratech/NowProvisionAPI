namespace NowProvisionAPI.WebApi.Features.Agents
{
    using NowProvisionAPI.Core.Dtos.Agent;
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

    public static class GetAgent
    {
        public class AgentQuery : IRequest<AgentDto>
        {
            public Guid AgentId { get; set; }

            public AgentQuery(Guid agentId)
            {
                AgentId = agentId;
            }
        }

        public class Handler : IRequestHandler<AgentQuery, AgentDto>
        {
            private readonly NowProvisionApiDbContext _db;
            private readonly IMapper _mapper;

            public Handler(NowProvisionApiDbContext db, IMapper mapper)
            {
                _mapper = mapper;
                _db = db;
            }

            public async Task<AgentDto> Handle(AgentQuery request, CancellationToken cancellationToken)
            {
                // add logger (and a try catch with logger so i can cap the unexpected info)........ unless this happens in my logger decorator that i am going to add?

                var result = await _db.Agents
                    .ProjectTo<AgentDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(a => a.AgentId == request.AgentId);

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