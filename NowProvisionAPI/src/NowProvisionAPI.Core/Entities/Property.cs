namespace NowProvisionAPI.Core.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Sieve.Attributes;

    [Table("Property")]
    public class Property
    {
        [Key]
        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public Guid PropertyId { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Slug { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string ContractType { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Country { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public bool? HideAddress { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Address { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string CityState { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string ZipCode { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Price { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Bedrooms { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Bathrooms { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Area { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Headline { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Description { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string ParkingSpaces { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Type { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string YearBuilt { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string BuiltArea { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string LotSize { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public Int32 Latitude { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public Int32 Longitude { get; set; }

        public DateTimeOffset CreatedUtc { get; set; }

        public DateTimeOffset UpdatedUtc { get; set; }

        public DateTimeOffset DeletedUtc { get; set; }

        public bool? IsDeleted { get; set; } = false;

        public string TenantId { get; set; }

        // add-on property marker - Do Not Delete This Comment
    }
}