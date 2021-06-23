namespace NowProvisionAPI.WebApi.Features.Agents.Mappings
{
    using NowProvisionAPI.Core.Dtos.Agent;
    using AutoMapper;
    using NowProvisionAPI.Core.Entities;

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