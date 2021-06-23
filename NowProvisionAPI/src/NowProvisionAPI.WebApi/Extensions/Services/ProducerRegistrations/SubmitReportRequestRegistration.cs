namespace NowProvisionAPI.WebApi.Extensions.Services.ProducerRegistrations
{
    using MassTransit;
    using MassTransit.RabbitMqTransport;
    using Messages;
    using RabbitMQ.Client;

    public static class SubmitReportRequestRegistration
    {
        public static void SubmitReportRequest(this IRabbitMqBusFactoryConfigurator cfg)
        {
            cfg.Message<ISendReportRequest>(e => e.SetEntityName("report-requests")); // name of the primary exchange
            cfg.Publish<ISendReportRequest>(e => e.ExchangeType = ExchangeType.Fanout); // primary exchange type
        }
    }
}