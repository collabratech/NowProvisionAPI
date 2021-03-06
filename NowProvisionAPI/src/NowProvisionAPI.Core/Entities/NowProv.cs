namespace NowProvisionAPI.Core.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Sieve.Attributes;

    [Table("NowProv")]
    public class NowProv
    {
        [Key]
        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public Guid Id { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Events { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string JobName { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public Int32 Status { get; set; }

        public DateTimeOffset CreatedUtc { get; set; }

        public DateTimeOffset UpdatedUtc { get; set; }

        public DateTimeOffset DeletedUtc { get; set; }

        public bool? IsDeleted { get; set; } = false;

        public string TenantId { get; set; }

        // add-on property marker - Do Not Delete This Comment
    }
}