namespace Ordering.Core.Dtos.Agent
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class AgentForCreationDto : AgentForManipulationDto
    {
        public Guid AgentId { get; set; } = Guid.NewGuid();

        // add-on property marker - Do Not Delete This Comment
    }
}