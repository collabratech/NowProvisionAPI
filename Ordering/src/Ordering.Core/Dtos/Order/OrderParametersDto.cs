namespace Ordering.Core.Dtos.Order
{
    using Ordering.Core.Dtos.Shared;

    public class OrderParametersDto : BasePaginationParameters
    {
        public string Filters { get; set; }
        public string SortOrder { get; set; }
    }
}