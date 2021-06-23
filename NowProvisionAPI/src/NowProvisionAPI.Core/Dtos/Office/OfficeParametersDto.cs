namespace NowProvisionAPI.Core.Dtos.Office
{
    using NowProvisionAPI.Core.Dtos.Shared;

    public class OfficeParametersDto : BasePaginationParameters
    {
        public string Filters { get; set; }
        public string SortOrder { get; set; }
    }
}