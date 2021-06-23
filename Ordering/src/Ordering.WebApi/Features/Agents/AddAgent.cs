namespace Ordering.WebApi.Features.Agents
{
    using Ordering.Core.Entities;
    using Ordering.Core.Dtos.Agent;
    using Ordering.Core.Exceptions;
    using Ordering.Infrastructure.Contexts;
    using Ordering.WebApi.Features.Agents.Validators;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public static class AddAgent
    {
        public class AddAgentCommand : IRequest<AgentDto>
        {
            public AgentForCreationDto AgentToAdd { get; set; }

            public AddAgentCommand(AgentForCreationDto agentToAdd)
            {
                AgentToAdd = agentToAdd;
            }
        }

        public class CustomCreateAgentValidation : AgentForManipulationDtoValidator<AgentForCreationDto>
        {
            public CustomCreateAgentValidation()
            {
            }
        }

        public class Handler : IRequestHandler<AddAgentCommand, AgentDto>
        {
            private readonly OrderingDbContext _db;
            private readonly IMapper _mapper;

            public Handler(OrderingDbContext db, IMapper mapper)
            {
                _mapper = mapper;
                _db = db;
            }

            public async Task<AgentDto> Handle(AddAgentCommand request, CancellationToken cancellationToken)
            {
                if (await _db.Agents.AnyAsync(a => a.AgentId == request.AgentToAdd.AgentId))
                {
                    throw new ConflictException("Agent already exists with this primary key.");
                }

                var agent = _mapper.Map<Agent> (request.AgentToAdd);
                _db.Agents.Add(agent);
                var saveSuccessful = await _db.SaveChangesAsync() > 0;

                if (saveSuccessful)
                {
                    return await _db.Agents
                        .ProjectTo<AgentDto>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync(a => a.AgentId == agent.AgentId);
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