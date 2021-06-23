namespace Ordering.Core.Dtos.Order
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class OrderDto 
    {
        public Guid Id { get; set; }
        public Int32 SubscriptionId { get; set; }
        public string ProductHandle { get; set; }
        public Int32 Status { get; set; }
        public Property Property { get; set; }
        public Office Office { get; set; }
        public Agent Agent { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset UpdatedUtc { get; set; }
        public DateTimeOffset DeletedUtc { get; set; }
        public bool? IsDeleted { get; set; }
        public string TenantId { get; set; }

        // add-on property marker - Do Not Delete This Comment
    }
}