namespace Ordering.Core.Dtos.Property
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class PropertyDto 
    {
        public Guid PropertyId { get; set; }
        public string Slug { get; set; }
        public string ContractType { get; set; }
        public string Country { get; set; }
        public bool? HideAddress { get; set; }
        public string Address { get; set; }
        public string CityState { get; set; }
        public string ZipCode { get; set; }
        public string Price { get; set; }
        public string Bedrooms { get; set; }
        public string Bathrooms { get; set; }
        public string Area { get; set; }
        public string Headline { get; set; }
        public string Description { get; set; }
        public string ParkingSpaces { get; set; }
        public string Type { get; set; }
        public string YearBuilt { get; set; }
        public string BuiltArea { get; set; }
        public string LotSize { get; set; }
        public Int32 Latitude { get; set; }
        public Int32 Longitude { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset UpdatedUtc { get; set; }
        public DateTimeOffset DeletedUtc { get; set; }
        public bool? IsDeleted { get; set; }
        public string TenantId { get; set; }

        // add-on property marker - Do Not Delete This Comment
    }
}