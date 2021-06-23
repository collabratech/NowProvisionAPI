namespace Ordering.WebApi.Features.Propertys
{
    using Ordering.Core.Entities;
    using Ordering.Core.Dtos.Property;
    using Ordering.Core.Exceptions;
    using Ordering.Infrastructure.Contexts;
    using Ordering.WebApi.Features.Propertys.Validators;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public static class AddProperty
    {
        public class AddPropertyCommand : IRequest<PropertyDto>
        {
            public PropertyForCreationDto PropertyToAdd { get; set; }

            public AddPropertyCommand(PropertyForCreationDto propertyToAdd)
            {
                PropertyToAdd = propertyToAdd;
            }
        }

        public class CustomCreatePropertyValidation : PropertyForManipulationDtoValidator<PropertyForCreationDto>
        {
            public CustomCreatePropertyValidation()
            {
            }
        }

        public class Handler : IRequestHandler<AddPropertyCommand, PropertyDto>
        {
            private readonly OrderingDbContext _db;
            private readonly IMapper _mapper;

            public Handler(OrderingDbContext db, IMapper mapper)
            {
                _mapper = mapper;
                _db = db;
            }

            public async Task<PropertyDto> Handle(AddPropertyCommand request, CancellationToken cancellationToken)
            {
                if (await _db.Propertys.AnyAsync(p => p.PropertyId == request.PropertyToAdd.PropertyId))
                {
                    throw new ConflictException("Property already exists with this primary key.");
                }

                var property = _mapper.Map<Property> (request.PropertyToAdd);
                _db.Propertys.Add(property);
                var saveSuccessful = await _db.SaveChangesAsync() > 0;

                if (saveSuccessful)
                {
                    return await _db.Propertys
                        .ProjectTo<PropertyDto>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync(p => p.PropertyId == property.PropertyId);
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