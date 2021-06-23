namespace NowProvisionAPI.WebApi.Features.NowProvs.Validators
{
    using NowProvisionAPI.Core.Dtos.NowProv;
    using FluentValidation;
    using System;

    public class NowProvForManipulationDtoValidator<T> : AbstractValidator<T> where T : NowProvForManipulationDto
    {
        public NowProvForManipulationDtoValidator()
        {
            // add fluent validation rules that should be shared between creation and update operations here
            //https://fluentvalidation.net/
        }
    }
}