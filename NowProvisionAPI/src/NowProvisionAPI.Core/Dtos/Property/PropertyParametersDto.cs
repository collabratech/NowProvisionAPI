namespace NowProvisionAPI.Core.Dtos.Property
{
    using NowProvisionAPI.Core.Dtos.Shared;

    public class PropertyParametersDto : BasePaginationParameters
    {
        public string Filters { get; set; }
        public string SortOrder { get; set; }
    }
}