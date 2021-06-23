namespace NowProvisionAPI.WebApi.Features.Propertys.Mappings
{
    using NowProvisionAPI.Core.Dtos.Property;
    using AutoMapper;
    using NowProvisionAPI.Core.Entities;

    public class PropertyProfile : Profile
    {
        public PropertyProfile()
        {
            //createmap<to this, from this>
            CreateMap<Property, PropertyDto>()
                .ReverseMap();
            CreateMap<PropertyForCreationDto, Property>();
            CreateMap<PropertyForUpdateDto, Property>()
                .ReverseMap();
        }
    }
}