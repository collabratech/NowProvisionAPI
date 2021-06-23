namespace NowProvisionAPI.Core.Dtos.NowProv
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class NowProvForCreationDto : NowProvForManipulationDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        // add-on property marker - Do Not Delete This Comment
    }
}