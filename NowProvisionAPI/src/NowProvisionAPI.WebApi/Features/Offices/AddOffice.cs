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

    public static class AddOffice
    {
        public class AddOfficeCommand : IRequest<OfficeDto>
        {
            public OfficeForCreationDto OfficeToAdd { get; set; }

            public AddOfficeCommand(OfficeForCreationDto officeToAdd)
            {
                OfficeToAdd = officeToAdd;
            }
        }

        public class CustomCreateOfficeValidation : OfficeForManipulationDtoValidator<OfficeForCreationDto>
        {
            public CustomCreateOfficeValidation()
            {
            }
        }

        public class Handler : IRequestHandler<AddOfficeCommand, OfficeDto>
        {
            private readonly NowProvisionApiDbContext _db;
            private readonly IMapper _mapper;

            public Handler(NowProvisionApiDbContext db, IMapper mapper)
            {
                _mapper = mapper;
                _db = db;
            }

            public async Task<OfficeDto> Handle(AddOfficeCommand request, CancellationToken cancellationToken)
            {
                if (await _db.Offices.AnyAsync(o => o.OfficeId == request.OfficeToAdd.OfficeId))
                {
                    throw new ConflictException("Office already exists with this primary key.");
                }

                var office = _mapper.Map<Office> (request.OfficeToAdd);
                _db.Offices.Add(office);
                var saveSuccessful = await _db.SaveChangesAsync() > 0;

                if (saveSuccessful)
                {
                    return await _db.Offices
                        .ProjectTo<OfficeDto>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync(o => o.OfficeId == office.OfficeId);
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