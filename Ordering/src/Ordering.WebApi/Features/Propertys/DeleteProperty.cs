namespace Ordering.WebApi.Features.Propertys
{
    using Ordering.Core.Entities;
    using Ordering.Core.Dtos.Property;
    using Ordering.Core.Exceptions;
    using Ordering.Infrastructure.Contexts;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public static class DeleteProperty
    {
        public class DeletePropertyCommand : IRequest<bool>
        {
            public Guid PropertyId { get; set; }

            public DeletePropertyCommand(Guid property)
            {
                PropertyId = property;
            }
        }

        public class Handler : IRequestHandler<DeletePropertyCommand, bool>
        {
            private readonly OrderingDbContext _db;
            private readonly IMapper _mapper;

            public Handler(OrderingDbContext db, IMapper mapper)
            {
                _mapper = mapper;
                _db = db;
            }

            public async Task<bool> Handle(DeletePropertyCommand request, CancellationToken cancellationToken)
            {
                // add logger (and a try catch with logger so i can cap the unexpected info)........ unless this happens in my logger decorator that i am going to add?

                var recordToDelete = await _db.Propertys
                    .FirstOrDefaultAsync(p => p.PropertyId == request.PropertyId);

                if (recordToDelete == null)
                {
                    // log error
                    throw new KeyNotFoundException();
                }

                _db.Propertys.Remove(recordToDelete);
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