namespace Ordering.Core.Dtos.Office
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public abstract class OfficeForManipulationDto 
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string CityState { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset UpdatedUtc { get; set; }
        public DateTimeOffset DeletedUtc { get; set; }
        public bool? IsDeleted { get; set; }
        public string TenantId { get; set; }

        // add-on property marker - Do Not Delete This Comment
    }
}