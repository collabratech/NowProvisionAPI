namespace Ordering.Core.Dtos.Order
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class OrderForCreationDto : OrderForManipulationDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        // add-on property marker - Do Not Delete This Comment
    }
}