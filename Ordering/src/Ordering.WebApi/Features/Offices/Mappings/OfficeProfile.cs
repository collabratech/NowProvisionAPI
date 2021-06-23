namespace Ordering.WebApi.Features.Offices.Mappings
{
    using Ordering.Core.Dtos.Office;
    using AutoMapper;
    using Ordering.Core.Entities;

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