namespace NowProvisionAPI.WebApi.Features.Offices
{
    using NowProvisionAPI.Core.Entities;
    using NowProvisionAPI.Core.Dtos.Office;
    using NowProvisionAPI.Core.Exceptions;
    using NowProvisionAPI.Infrastructure.Contexts;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public static class DeleteOffice
    {
        public class DeleteOfficeCommand : IRequest<bool>
        {
            public Guid OfficeId { get; set; }

            public DeleteOfficeCommand(Guid office)
            {
                OfficeId = office;
            }
        }

        public class Handler : IRequestHandler<DeleteOfficeCommand, bool>
        {
            private readonly NowProvisionApiDbContext _db;
            private readonly IMapper _mapper;

            public Handler(NowProvisionApiDbContext db, IMapper mapper)
            {
                _mapper = mapper;
                _db = db;
            }

            public async Task<bool> Handle(DeleteOfficeCommand request, CancellationToken cancellationToken)
            {
                // add logger (and a try catch with logger so i can cap the unexpected info)........ unless this happens in my logger decorator that i am going to add?

                var recordToDelete = await _db.Offices
                    .FirstOrDefaultAsync(o => o.OfficeId == request.OfficeId);

                if (recordToDelete == null)
                {
                    // log error
                    throw new KeyNotFoundException();
                }

                _db.Offices.Remove(recordToDelete);
                var saveSuccessful = await _db.SaveChangesAsync() > 0;

                if (!saveSuccessful)
                {
                    // add log
                    throw new Exception("Unable to save the new record. Please check the logs for more information.");
                }

                return true;
            }
        }
    }
}