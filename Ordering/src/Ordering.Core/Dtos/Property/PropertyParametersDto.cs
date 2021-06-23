namespace Ordering.Core.Dtos.Property
{
    using Ordering.Core.Dtos.Shared;

    public class PropertyParametersDto : BasePaginationParameters
    {
        public string Filters { get; set; }
        public string SortOrder { get; set; }
    }
}