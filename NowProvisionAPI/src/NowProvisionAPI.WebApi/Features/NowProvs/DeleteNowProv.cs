namespace NowProvisionAPI.WebApi.Features.NowProvs
{
    using NowProvisionAPI.Core.Entities;
    using NowProvisionAPI.Core.Dtos.NowProv;
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

    public static class DeleteNowProv
    {
        public class DeleteNowProvCommand : IRequest<bool>
        {
            public Guid Id { get; set; }

            public DeleteNowProvCommand(Guid nowProv)
            {
                Id = nowProv;
            }
        }

        public class Handler : IRequestHandler<DeleteNowProvCommand, bool>
        {
            private readonly NowProvisionApiDbContext _db;
            private readonly IMapper _mapper;

            public Handler(NowProvisionApiDbContext db, IMapper mapper)
            {
                _mapper = mapper;
                _db = db;
            }

            public async Task<bool> Handle(DeleteNowProvCommand request, CancellationToken cancellationToken)
            {
                // add logger (and a try catch with logger so i can cap the unexpected info)........ unless this happens in my logger decorator that i am going to add?

                var recordToDelete = await _db.NowProvs
                    .FirstOrDefaultAsync(n => n.Id == request.Id);

                if (recordToDelete == null)
                {
                    // log error
                    throw new KeyNotFoundException();
                }

                _db.NowProvs.Remove(recordToDelete);
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