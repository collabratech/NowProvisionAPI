namespace NowProvisionAPI.WebApi.Features.Propertys.Validators
{
    using NowProvisionAPI.Core.Dtos.Property;
    using FluentValidation;
    using System;

    public class PropertyForManipulationDtoValidator<T> : AbstractValidator<T> where T : PropertyForManipulationDto
    {
        public PropertyForManipulationDtoValidator()
        {
            // add fluent validation rules that should be shared between creation and update operations here
            //https://fluentvalidation.net/
        }
    }
}