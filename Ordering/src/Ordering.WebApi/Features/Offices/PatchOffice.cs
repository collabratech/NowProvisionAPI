namespace Ordering.WebApi.Features.Offices
{
    using Ordering.Core.Entities;
    using Ordering.Core.Dtos.Office;
    using Ordering.Core.Exceptions;
    using Ordering.Infrastructure.Contexts;
    using Ordering.WebApi.Features.Offices.Validators;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public static class PatchOffice
    {
        public class PatchOfficeCommand : IRequest<bool>
        {
            public Guid OfficeId { get; set; }
            public JsonPatchDocument<OfficeForUpdateDto> PatchDoc { get; set; }

            public PatchOfficeCommand(Guid office, JsonPatchDocument<OfficeForUpdateDto> patchDoc)
            {
                OfficeId = office;
                PatchDoc = patchDoc;
            }
        }

        public class CustomPatchOfficeValidation : OfficeForManipulationDtoValidator<OfficeForUpdateDto>
        {
            public CustomPatchOfficeValidation()
            {
            }
        }

        public class Handler : IRequestHandler<PatchOfficeCommand, bool>
        {
            private readonly OrderingDbContext _db;
            private readonly IMapper _mapper;

            public Handler(OrderingDbContext db, IMapper mapper)
            {
                _mapper = mapper;
                _db = db;
            }

            public async Task<bool> Handle(PatchOfficeCommand request, CancellationToken cancellationToken)
            {
                // add logger or use decorator
                if (request.PatchDoc == null)
                {
                    // log error
                    throw new ApiException("Invalid patch document.");
                }

                var officeToUpdate = await _db.Offices
                    .FirstOrDefaultAsync(o => o.OfficeId == request.OfficeId);

                if (officeToUpdate == null)
                {
                    // log error
                    throw new KeyNotFoundException();
                }

                var officeToPatch = _mapper.Map<OfficeForUpdateDto>(officeToUpdate); // map the office we got from the database to an updatable office model
                request.PatchDoc.ApplyTo(officeToPatch); // apply patchdoc updates to the updatable office

                var validationResults = new CustomPatchOfficeValidation().Validate(officeToPatch);
                if (!validationResults.IsValid)
                {
                    throw new ValidationException(validationResults.Errors);
                }

                _mapper.Map(officeToPatch, officeToUpdate);
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