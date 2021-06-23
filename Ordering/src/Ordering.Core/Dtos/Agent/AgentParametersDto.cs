namespace Ordering.Core.Dtos.Agent
{
    using Ordering.Core.Dtos.Shared;

    public class AgentParametersDto : BasePaginationParameters
    {
        public string Filters { get; set; }
        public string SortOrder { get; set; }
    }
}