namespace NowProvisionAPI.WebApi.Features.NowProvs.Mappings
{
    using NowProvisionAPI.Core.Dtos.NowProv;
    using AutoMapper;
    using NowProvisionAPI.Core.Entities;

    public class NowProvProfile : Profile
    {
        public NowProvProfile()
        {
            //createmap<to this, from this>
            CreateMap<NowProv, NowProvDto>()
                .ReverseMap();
            CreateMap<NowProvForCreationDto, NowProv>();
            CreateMap<NowProvForUpdateDto, NowProv>()
                .ReverseMap();
        }
    }
}