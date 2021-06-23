namespace Ordering.WebApi.Features.Agents.Mappings
{
    using Ordering.Core.Dtos.Agent;
    using AutoMapper;
    using Ordering.Core.Entities;

    public class AgentProfile : Profile
    {
        public AgentProfile()
        {
            //createmap<to this, from this>
            CreateMap<Agent, AgentDto>()
                .ReverseMap();
            CreateMap<AgentForCreationDto, Agent>();
            CreateMap<AgentForUpdateDto, Agent>()
                .ReverseMap();
        }
    }
}