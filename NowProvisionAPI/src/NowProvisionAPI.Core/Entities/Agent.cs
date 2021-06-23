namespace NowProvisionAPI.Core.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Sieve.Attributes;

    [Table("Agent")]
    public class Agent
    {
        [Key]
        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public Guid AgentId { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Name { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Phone { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Email { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Website { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Twitter { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Facebook { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string LinkedIn { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string License { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string LicenseIcon { get; set; }

        public DateTimeOffset CreatedUtc { get; set; }

        public DateTimeOffset UpdatedUtc { get; set; }

        public DateTimeOffset DeletedUtc { get; set; }

        public bool? IsDeleted { get; set; } = false;

        public string TenantId { get; set; }

        // add-on property marker - Do Not Delete This Comment
    }
}