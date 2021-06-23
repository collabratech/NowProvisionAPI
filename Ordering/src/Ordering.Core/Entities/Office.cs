namespace Ordering.Core.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Sieve.Attributes;

    [Table("Office")]
    public class Office
    {
        [Key]
        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public Guid OfficeId { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Name { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Address { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string CityState { get; set; }

        public DateTimeOffset CreatedUtc { get; set; }

        public DateTimeOffset UpdatedUtc { get; set; }

        public DateTimeOffset DeletedUtc { get; set; }

        public bool? IsDeleted { get; set; } = false;

        public string TenantId { get; set; }

        // add-on property marker - Do Not Delete This Comment
    }
}