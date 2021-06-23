namespace Ordering.WebApi.Features.Propertys.Mappings
{
    using Ordering.Core.Dtos.Property;
    using AutoMapper;
    using Ordering.Core.Entities;

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