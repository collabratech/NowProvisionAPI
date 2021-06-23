namespace NowProvisionAPI.Core.Dtos.NowProv
{
    using NowProvisionAPI.Core.Dtos.Shared;

    public class NowProvParametersDto : BasePaginationParameters
    {
        public string Filters { get; set; }
        public string SortOrder { get; set; }
    }
}