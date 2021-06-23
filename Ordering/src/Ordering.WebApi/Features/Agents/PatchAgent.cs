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
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public static class PatchAgent
    {
        public class PatchAgentCommand : IRequest<bool>
        {
            public Guid AgentId { get; set; }
            public JsonPatchDocument<AgentForUpdateDto> PatchDoc { get; set; }

            public PatchAgentCommand(Guid agent, JsonPatchDocument<AgentForUpdateDto> patchDoc)
            {
                AgentId = agent;
                PatchDoc = patchDoc;
            }
        }

        public class CustomPatchAgentValidation : AgentForManipulationDtoValidator<AgentForUpdateDto>
        {
            public CustomPatchAgentValidation()
            {
            }
        }

        public class Handler : IRequestHandler<PatchAgentCommand, bool>
        {
            private readonly OrderingDbContext _db;
            private readonly IMapper _mapper;

            public Handler(OrderingDbContext db, IMapper mapper)
            {
                _mapper = mapper;
                _db = db;
            }

            public async Task<bool> Handle(PatchAgentCommand request, CancellationToken cancellationToken)
            {
                // add logger or use decorator
                if (request.PatchDoc == null)
                {
                    // log error
                    throw new ApiException("Invalid patch document.");
                }

                var agentToUpdate = await _db.Agents
                    .FirstOrDefaultAsync(a => a.AgentId == request.AgentId);

                if (agentToUpdate == null)
                {
                    // log error
                    throw new KeyNotFoundException();
                }

                var agentToPatch = _mapper.Map<AgentForUpdateDto>(agentToUpdate); // map the agent we got from the database to an updatable agent model
                request.PatchDoc.ApplyTo(agentToPatch); // apply patchdoc updates to the updatable agent

                var validationResults = new CustomPatchAgentValidation().Validate(agentToPatch);
                if (!validationResults.IsValid)
                {
                    throw new ValidationException(validationResults.Errors);
                }

                _mapper.Map(agentToPatch, agentToUpdate);
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