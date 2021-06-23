namespace Ordering.WebApi.Features.Orders
{
    using Ordering.Core.Entities;
    using Ordering.Core.Dtos.Order;
    using Ordering.Core.Exceptions;
    using Ordering.Infrastructure.Contexts;
    using Ordering.WebApi.Features.Orders.Validators;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public static class PatchOrder
    {
        public class PatchOrderCommand : IRequest<bool>
        {
            public Guid Id { get; set; }
            public JsonPatchDocument<OrderForUpdateDto> PatchDoc { get; set; }

            public PatchOrderCommand(Guid order, JsonPatchDocument<OrderForUpdateDto> patchDoc)
            {
                Id = order;
                PatchDoc = patchDoc;
            }
        }

        public class CustomPatchOrderValidation : OrderForManipulationDtoValidator<OrderForUpdateDto>
        {
            public CustomPatchOrderValidation()
            {
            }
        }

        public class Handler : IRequestHandler<PatchOrderCommand, bool>
        {
            private readonly OrderingDbContext _db;
            private readonly IMapper _mapper;

            public Handler(OrderingDbContext db, IMapper mapper)
            {
                _mapper = mapper;
                _db = db;
            }

            public async Task<bool> Handle(PatchOrderCommand request, CancellationToken cancellationToken)
            {
                // add logger or use decorator
                if (request.PatchDoc == null)
                {
                    // log error
                    throw new ApiException("Invalid patch document.");
                }

                var orderToUpdate = await _db.Orders
                    .FirstOrDefaultAsync(o => o.Id == request.Id);

                if (orderToUpdate == null)
                {
                    // log error
                    throw new KeyNotFoundException();
                }

                var orderToPatch = _mapper.Map<OrderForUpdateDto>(orderToUpdate); // map the order we got from the database to an updatable order model
                request.PatchDoc.ApplyTo(orderToPatch); // apply patchdoc updates to the updatable order

                var validationResults = new CustomPatchOrderValidation().Validate(orderToPatch);
                if (!validationResults.IsValid)
                {
                    throw new ValidationException(validationResults.Errors);
                }

                _mapper.Map(orderToPatch, orderToUpdate);
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