namespace NowProvisionAPI.WebApi.Features.Offices.Mappings
{
    using NowProvisionAPI.Core.Dtos.Office;
    using AutoMapper;
    using NowProvisionAPI.Core.Entities;

    public class OfficeProfile : Profile
    {
        public OfficeProfile()
        {
            //createmap<to this, from this>
            CreateMap<Office, OfficeDto>()
                .ReverseMap();
            CreateMap<OfficeForCreationDto, Office>();
            CreateMap<OfficeForUpdateDto, Office>()
                .ReverseMap();
        }
    }
}