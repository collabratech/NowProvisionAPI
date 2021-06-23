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

    public static class UpdateAgent
    {
        public class UpdateAgentCommand : IRequest<bool>
        {
            public Guid AgentId { get; set; }
            public AgentForUpdateDto AgentToUpdate { get; set; }

            public UpdateAgentCommand(Guid agent, AgentForUpdateDto agentToUpdate)
            {
                AgentId = agent;
                AgentToUpdate = agentToUpdate;
            }
        }

        public class CustomUpdateAgentValidation : AgentForManipulationDtoValidator<AgentForUpdateDto>
        {
            public CustomUpdateAgentValidation()
            {
            }
        }

        public class Handler : IRequestHandler<UpdateAgentCommand, bool>
        {
            private readonly OrderingDbContext _db;
            private readonly IMapper _mapper;

            public Handler(OrderingDbContext db, IMapper mapper)
            {
                _mapper = mapper;
                _db = db;
            }

            public async Task<bool> Handle(UpdateAgentCommand request, CancellationToken cancellationToken)
            {
                // add logger or use decorator

                var recordToUpdate = await _db.Agents
                    .FirstOrDefaultAsync(a => a.AgentId == request.AgentId);

                if (recordToUpdate == null)
                {
                    // log error
                    throw new KeyNotFoundException();
                }

                _mapper.Map(request.AgentToUpdate, recordToUpdate);
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