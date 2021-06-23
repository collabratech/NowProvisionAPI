namespace Ordering.Core.Dtos.Property
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class PropertyForCreationDto : PropertyForManipulationDto
    {
        public Guid PropertyId { get; set; } = Guid.NewGuid();

        // add-on property marker - Do Not Delete This Comment
    }
}