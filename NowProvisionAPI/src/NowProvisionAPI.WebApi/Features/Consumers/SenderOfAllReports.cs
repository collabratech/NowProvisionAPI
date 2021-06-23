namespace NowProvisionAPI.WebApi.Features.Consumers
{
    using AutoMapper;
    using MassTransit;
    using Messages;
    using System.Threading.Tasks;
    using NowProvisionAPI.Infrastructure.Contexts;

    public class SenderOfAllReports : IConsumer<ISendReportRequest>
    {
        private readonly IMapper _mapper;
        private readonly NowProvisionApiDbContext _db;

        public SenderOfAllReports(NowProvisionApiDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public class SenderOfAllReportsProfile : Profile
        {
            public SenderOfAllReportsProfile()
            {
                //createmap<to this, from this>
            }
        }

        public Task Consume(ConsumeContext<ISendReportRequest> context)
        {
            // do work here

            return Task.CompletedTask;
        }
    }
}