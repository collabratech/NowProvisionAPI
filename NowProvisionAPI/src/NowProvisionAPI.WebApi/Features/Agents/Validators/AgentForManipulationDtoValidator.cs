namespace NowProvisionAPI.WebApi.Features.Agents.Validators
{
    using NowProvisionAPI.Core.Dtos.Agent;
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