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
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public static class AddNowProv
    {
        public class AddNowProvCommand : IRequest<NowProvDto>
        {
            public NowProvForCreationDto NowProvToAdd { get; set; }

            public AddNowProvCommand(NowProvForCreationDto nowProvToAdd)
            {
                NowProvToAdd = nowProvToAdd;
            }
        }

        public class CustomCreateNowProvValidation : NowProvForManipulationDtoValidator<NowProvForCreationDto>
        {
            public CustomCreateNowProvValidation()
            {
            }
        }

        public class Handler : IRequestHandler<AddNowProvCommand, NowProvDto>
        {
            private readonly NowProvisionApiDbContext _db;
            private readonly IMapper _mapper;

            public Handler(NowProvisionApiDbContext db, IMapper mapper)
            {
                _mapper = mapper;
                _db = db;
            }

            public async Task<NowProvDto> Handle(AddNowProvCommand request, CancellationToken cancellationToken)
            {
                if (await _db.NowProvs.AnyAsync(n => n.Id == request.NowProvToAdd.Id))
                {
                    throw new ConflictException("NowProv already exists with this primary key.");
                }

                var nowProv = _mapper.Map<NowProv> (request.NowProvToAdd);
                _db.NowProvs.Add(nowProv);
                var saveSuccessful = await _db.SaveChangesAsync() > 0;

                if (saveSuccessful)
                {
                    return await _db.NowProvs
                        .ProjectTo<NowProvDto>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync(n => n.Id == nowProv.Id);
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