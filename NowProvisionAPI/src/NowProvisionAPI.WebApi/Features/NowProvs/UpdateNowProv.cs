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

    public static class UpdateNowProv
    {
        public class UpdateNowProvCommand : IRequest<bool>
        {
            public Guid Id { get; set; }
            public NowProvForUpdateDto NowProvToUpdate { get; set; }

            public UpdateNowProvCommand(Guid nowProv, NowProvForUpdateDto nowProvToUpdate)
            {
                Id = nowProv;
                NowProvToUpdate = nowProvToUpdate;
            }
        }

        public class CustomUpdateNowProvValidation : NowProvForManipulationDtoValidator<NowProvForUpdateDto>
        {
            public CustomUpdateNowProvValidation()
            {
            }
        }

        public class Handler : IRequestHandler<UpdateNowProvCommand, bool>
        {
            private readonly NowProvisionApiDbContext _db;
            private readonly IMapper _mapper;

            public Handler(NowProvisionApiDbContext db, IMapper mapper)
            {
                _mapper = mapper;
                _db = db;
            }

            public async Task<bool> Handle(UpdateNowProvCommand request, CancellationToken cancellationToken)
            {
                // add logger or use decorator

                var recordToUpdate = await _db.NowProvs
                    .FirstOrDefaultAsync(n => n.Id == request.Id);

                if (recordToUpdate == null)
                {
                    // log error
                    throw new KeyNotFoundException();
                }

                _mapper.Map(request.NowProvToUpdate, recordToUpdate);
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