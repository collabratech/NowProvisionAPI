namespace Ordering.Core.Dtos.Office
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class OfficeForCreationDto : OfficeForManipulationDto
    {
        public Guid OfficeId { get; set; } = Guid.NewGuid();

        // add-on property marker - Do Not Delete This Comment
    }
}