namespace NowProvisionAPI.WebApi.Features.Offices
{
    using NowProvisionAPI.Core.Entities;
    using NowProvisionAPI.Core.Dtos.Office;
    using NowProvisionAPI.Core.Exceptions;
    using NowProvisionAPI.Infrastructure.Contexts;
    using NowProvisionAPI.WebApi.Features.Offices.Validators;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public static class UpdateOffice
    {
        public class UpdateOfficeCommand : IRequest<bool>
        {
            public Guid OfficeId { get; set; }
            public OfficeForUpdateDto OfficeToUpdate { get; set; }

            public UpdateOfficeCommand(Guid office, OfficeForUpdateDto officeToUpdate)
            {
                OfficeId = office;
                OfficeToUpdate = officeToUpdate;
            }
        }

        public class CustomUpdateOfficeValidation : OfficeForManipulationDtoValidator<OfficeForUpdateDto>
        {
            public CustomUpdateOfficeValidation()
            {
            }
        }

        public class Handler : IRequestHandler<UpdateOfficeCommand, bool>
        {
            private readonly NowProvisionApiDbContext _db;
            private readonly IMapper _mapper;

            public Handler(NowProvisionApiDbContext db, IMapper mapper)
            {
                _mapper = mapper;
                _db = db;
            }

            public async Task<bool> Handle(UpdateOfficeCommand request, CancellationToken cancellationToken)
            {
                // add logger or use decorator

                var recordToUpdate = await _db.Offices
                    .FirstOrDefaultAsync(o => o.OfficeId == request.OfficeId);

                if (recordToUpdate == null)
                {
                    // log error
                    throw new KeyNotFoundException();
                }

                _mapper.Map(request.OfficeToUpdate, recordToUpdate);
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