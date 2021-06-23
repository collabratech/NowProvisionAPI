namespace NowProvisionAPI.Core.Dtos.Agent
{
    using NowProvisionAPI.Core.Dtos.Shared;

    public class AgentParametersDto : BasePaginationParameters
    {
        public string Filters { get; set; }
        public string SortOrder { get; set; }
    }
}