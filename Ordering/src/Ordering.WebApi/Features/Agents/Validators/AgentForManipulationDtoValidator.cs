namespace Ordering.WebApi.Features.Agents.Validators
{
    using Ordering.Core.Dtos.Agent;
    using FluentValidation;
    using System;

    public class AgentForManipulationDtoValidator<T> : AbstractValidator<T> where T : AgentForManipulationDto
    {
        public AgentForManipulationDtoValidator()
        {
            // add fluent validation rules that should be shared between creation and update operations here
            //https://fluentvalidation.net/
        }
    }
}