namespace NowProvisionAPI.WebApi.Features.NowProvs
{
    using NowProvisionAPI.Core.Entities;
    using NowProvisionAPI.Core.Dtos.NowProv;
    using NowProvisionAPI.Core.Exceptions;
    using NowProvisionAPI.Infrastructure.Contexts;
    using NowProvisionAPI.WebApi.Features.NowProvs.Validators;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public static class PatchNowProv
    {
        public class PatchNowProvCommand : IRequest<bool>
        {
            public Guid Id { get; set; }
            public JsonPatchDocument<NowProvForUpdateDto> PatchDoc { get; set; }

            public PatchNowProvCommand(Guid nowProv, JsonPatchDocument<NowProvForUpdateDto> patchDoc)
            {
                Id = nowProv;
                PatchDoc = patchDoc;
            }
        }

        public class CustomPatchNowProvValidation : NowProvForManipulationDtoValidator<NowProvForUpdateDto>
        {
            public CustomPatchNowProvValidation()
            {
            }
        }

        public class Handler : IRequestHandler<PatchNowProvCommand, bool>
        {
            private readonly NowProvisionApiDbContext _db;
            private readonly IMapper _mapper;

            public Handler(NowProvisionApiDbContext db, IMapper mapper)
            {
                _mapper = mapper;
                _db = db;
            }

            public async Task<bool> Handle(PatchNowProvCommand request, CancellationToken cancellationToken)
            {
                // add logger or use decorator
                if (request.PatchDoc == null)
                {
                    // log error
                    throw new ApiException("Invalid patch document.");
                }

                var nowProvToUpdate = await _db.NowProvs
                    .FirstOrDefaultAsync(n => n.Id == request.Id);

                if (nowProvToUpdate == null)
                {
                    // log error
                    throw new KeyNotFoundException();
                }

                var nowProvToPatch = _mapper.Map<NowProvForUpdateDto>(nowProvToUpdate); // map the nowProv we got from the database to an updatable nowProv model
                request.PatchDoc.ApplyTo(nowProvToPatch); // apply patchdoc updates to the updatable nowProv

                var validationResults = new CustomPatchNowProvValidation().Validate(nowProvToPatch);
                if (!validationResults.IsValid)
                {
                    throw new ValidationException(validationResults.Errors);
                }

                _mapper.Map(nowProvToPatch, nowProvToUpdate);
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