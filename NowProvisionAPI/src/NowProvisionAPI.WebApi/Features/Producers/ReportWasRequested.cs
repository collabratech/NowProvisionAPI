namespace NowProvisionAPI.WebApi.Features.Producers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MassTransit;
    using Messages;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using NowProvisionAPI.Infrastructure.Contexts;

    public static class ReportWasRequested
    {
        public class ReportWasRequestedCommand : IRequest<bool>
        {
            public string SomeContentToPutInMessage { get; set; }

            public ReportWasRequestedCommand(string someContentToPutInMessage)
            {
                SomeContentToPutInMessage = someContentToPutInMessage;
            }
        }

        public class Handler : IRequestHandler<ReportWasRequestedCommand, bool>
        {
            private readonly IPublishEndpoint _publishEndpoint;
            private readonly IMapper _mapper;
            private readonly NowProvisionApiDbContext _db;

            public Handler(NowProvisionApiDbContext db, IMapper mapper, IPublishEndpoint publishEndpoint)
            {
                _publishEndpoint = publishEndpoint;
                _mapper = mapper;
                _db = db;
            }

            public class ReportWasRequestedCommandProfile : Profile
            {
                public ReportWasRequestedCommandProfile()
                {
                    //createmap<to this, from this>
                }
            }

            public async Task<bool> Handle(ReportWasRequestedCommand request, CancellationToken cancellationToken)
            {
                var message = new
                {
                    // map content to message here or with automapper
                };
                await _publishEndpoint.Publish<ISendReportRequest>(message);

                return true;
            }
        }
    }
}