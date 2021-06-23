namespace NowProvisionAPI.WebApi.Features.Propertys
{
    using NowProvisionAPI.Core.Entities;
    using NowProvisionAPI.Core.Dtos.Property;
    using NowProvisionAPI.Core.Exceptions;
    using NowProvisionAPI.Infrastructure.Contexts;
    using NowProvisionAPI.WebApi.Features.Propertys.Validators;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public static class PatchProperty
    {
        public class PatchPropertyCommand : IRequest<bool>
        {
            public Guid PropertyId { get; set; }
            public JsonPatchDocument<PropertyForUpdateDto> PatchDoc { get; set; }

            public PatchPropertyCommand(Guid property, JsonPatchDocument<PropertyForUpdateDto> patchDoc)
            {
                PropertyId = property;
                PatchDoc = patchDoc;
            }
        }

        public class CustomPatchPropertyValidation : PropertyForManipulationDtoValidator<PropertyForUpdateDto>
        {
            public CustomPatchPropertyValidation()
            {
            }
        }

        public class Handler : IRequestHandler<PatchPropertyCommand, bool>
        {
            private readonly NowProvisionApiDbContext _db;
            private readonly IMapper _mapper;

            public Handler(NowProvisionApiDbContext db, IMapper mapper)
            {
                _mapper = mapper;
                _db = db;
            }

            public async Task<bool> Handle(PatchPropertyCommand request, CancellationToken cancellationToken)
            {
                // add logger or use decorator
                if (request.PatchDoc == null)
                {
                    // log error
                    throw new ApiException("Invalid patch document.");
                }

                var propertyToUpdate = await _db.Propertys
                    .FirstOrDefaultAsync(p => p.PropertyId == request.PropertyId);

                if (propertyToUpdate == null)
                {
                    // log error
                    throw new KeyNotFoundException();
                }

                var propertyToPatch = _mapper.Map<PropertyForUpdateDto>(propertyToUpdate); // map the property we got from the database to an updatable property model
                request.PatchDoc.ApplyTo(propertyToPatch); // apply patchdoc updates to the updatable property

                var validationResults = new CustomPatchPropertyValidation().Validate(propertyToPatch);
                if (!validationResults.IsValid)
                {
                    throw new ValidationException(validationResults.Errors);
                }

                _mapper.Map(propertyToPatch, propertyToUpdate);
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