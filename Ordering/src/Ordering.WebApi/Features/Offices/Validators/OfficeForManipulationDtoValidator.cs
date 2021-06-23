namespace Ordering.WebApi.Features.Offices.Validators
{
    using Ordering.Core.Dtos.Office;
    using FluentValidation;
    using System;

    public class OfficeForManipulationDtoValidator<T> : AbstractValidator<T> where T : OfficeForManipulationDto
    {
        public OfficeForManipulationDtoValidator()
        {
            // add fluent validation rules that should be shared between creation and update operations here
            //https://fluentvalidation.net/
        }
    }
}