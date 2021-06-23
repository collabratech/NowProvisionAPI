namespace Ordering.WebApi.Features.Orders.Validators
{
    using Ordering.Core.Dtos.Order;
    using FluentValidation;
    using System;

    public class OrderForManipulationDtoValidator<T> : AbstractValidator<T> where T : OrderForManipulationDto
    {
        public OrderForManipulationDtoValidator()
        {
            // add fluent validation rules that should be shared between creation and update operations here
            //https://fluentvalidation.net/
        }
    }
}