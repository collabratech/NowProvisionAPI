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

    public static class UpdateProperty
    {
        public class UpdatePropertyCommand : IRequest<bool>
        {
            public Guid PropertyId { get; set; }
            public PropertyForUpdateDto PropertyToUpdate { get; set; }

            public UpdatePropertyCommand(Guid property, PropertyForUpdateDto propertyToUpdate)
            {
                PropertyId = property;
                PropertyToUpdate = propertyToUpdate;
            }
        }

        public class CustomUpdatePropertyValidation : PropertyForManipulationDtoValidator<PropertyForUpdateDto>
        {
            public CustomUpdatePropertyValidation()
            {
            }
        }

        public class Handler : IRequestHandler<UpdatePropertyCommand, bool>
        {
            private readonly OrderingDbContext _db;
            private readonly IMapper _mapper;

            public Handler(OrderingDbContext db, IMapper mapper)
            {
                _mapper = mapper;
                _db = db;
            }

            public async Task<bool> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
            {
                // add logger or use decorator

                var recordToUpdate = await _db.Propertys
                    .FirstOrDefaultAsync(p => p.PropertyId == request.PropertyId);

                if (recordToUpdate == null)
                {
                    // log error
                    throw new KeyNotFoundException();
                }

                _mapper.Map(request.PropertyToUpdate, recordToUpdate);
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