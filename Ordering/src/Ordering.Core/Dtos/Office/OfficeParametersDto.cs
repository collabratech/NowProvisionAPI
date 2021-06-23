namespace Ordering.Core.Dtos.Office
{
    using Ordering.Core.Dtos.Shared;

    public class OfficeParametersDto : BasePaginationParameters
    {
        public string Filters { get; set; }
        public string SortOrder { get; set; }
    }
}