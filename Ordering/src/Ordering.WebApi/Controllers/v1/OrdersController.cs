namespace Ordering.WebApi.Controllers.v1
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using System.Threading.Tasks;
    using Ordering.Core.Dtos.Order;
    using Ordering.Core.Wrappers;
    using System.Threading;
    using MediatR;
    using Ordering.WebApi.Features.Orders;

    [ApiController]
    [Route("api/Orders")]
    [ApiVersion("1.0")]
    public class OrdersController: ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        /// <summary>
        /// Gets a list of all Orders.
        /// </summary>
        /// <response code="200">Order list returned successfully.</response>
        /// <response code="400">Order has missing/invalid values.</response>
        /// <response code="401">This request was not able to be authenticated.</response>
        /// <response code="403">The required permissions to access this resource were not present in the given request.</response>
        /// <response code="500">There was an error on the server while creating the Order.</response>
        /// <remarks>
        /// Requests can be narrowed down with a variety of query string values:
        /// ## Query String Parameters
        /// - **PageNumber**: An integer value that designates the page of records that should be returned.
        /// - **PageSize**: An integer value that designates the number of records returned on the given page that you would like to return. This value is capped by the internal MaxPageSize.
        /// - **SortOrder**: A comma delimited ordered list of property names to sort by. Adding a `-` before the name switches to sorting descendingly.
        /// - **Filters**: A comma delimited list of fields to filter by formatted as `{Name}{Operator}{Value}` where
        ///     - {Name} is the name of a filterable property. You can also have multiple names (for OR logic) by enclosing them in brackets and using a pipe delimiter, eg. `(LikeCount|CommentCount)>10` asks if LikeCount or CommentCount is >10
        ///     - {Operator} is one of the Operators below
        ///     - {Value} is the value to use for filtering. You can also have multiple values (for OR logic) by using a pipe delimiter, eg.`Title@= new|hot` will return posts with titles that contain the text "new" or "hot"
        ///
        ///    | Operator | Meaning                       | Operator  | Meaning                                      |
        ///    | -------- | ----------------------------- | --------- | -------------------------------------------- |
        ///    | `==`     | Equals                        |  `!@=`    | Does not Contains                            |
        ///    | `!=`     | Not equals                    |  `!_=`    | Does not Starts with                         |
        ///    | `>`      | Greater than                  |  `@=*`    | Case-insensitive string Contains             |
        ///    | `&lt;`   | Less than                     |  `_=*`    | Case-insensitive string Starts with          |
        ///    | `>=`     | Greater than or equal to      |  `==*`    | Case-insensitive string Equals               |
        ///    | `&lt;=`  | Less than or equal to         |  `!=*`    | Case-insensitive string Not equals           |
        ///    | `@=`     | Contains                      |  `!@=*`   | Case-insensitive string does not Contains    |
        ///    | `_=`     | Starts with                   |  `!_=*`   | Case-insensitive string does not Starts with |
        /// </remarks>
        [ProducesResponseType(typeof(Response<IEnumerable<OrderDto>>), 200)]
        [ProducesResponseType(typeof(Response<>), 400)]
        [ProducesResponseType(typeof(Response<>), 401)]
        [ProducesResponseType(typeof(Response<>), 403)]
        [ProducesResponseType(500)]
        [Authorize(Policy = "CanReadOrder")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpGet(Name = "GetOrders")]
        public async Task<IActionResult> GetOrders([FromQuery] OrderParametersDto orderParametersDto)
        {
            // add error handling
            var query = new GetOrderList.OrderListQuery(orderParametersDto);
            var queryResponse = await _mediator.Send(query);

            var paginationMetadata = new
            {
                totalCount = queryResponse.TotalCount,
                pageSize = queryResponse.PageSize,
                currentPageSize = queryResponse.CurrentPageSize,
                currentStartIndex = queryResponse.CurrentStartIndex,
                currentEndIndex = queryResponse.CurrentEndIndex,
                pageNumber = queryResponse.PageNumber,
                totalPages = queryResponse.TotalPages,
                hasPrevious = queryResponse.HasPrevious,
                hasNext = queryResponse.HasNext
            };

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));

            var response = new Response<IEnumerable<OrderDto>>(queryResponse);
            return Ok(response);
        }
        
        /// <summary>
        /// Gets a single Order by ID.
        /// </summary>
        /// <response code="200">Order record returned successfully.</response>
        /// <response code="400">Order has missing/invalid values.</response>
        /// <response code="401">This request was not able to be authenticated.</response>
        /// <response code="403">The required permissions to access this resource were not present in the given request.</response>
        /// <response code="500">There was an error on the server while creating the Order.</response>
        [ProducesResponseType(typeof(Response<OrderDto>), 200)]
        [ProducesResponseType(typeof(Response<>), 400)]
        [ProducesResponseType(typeof(Response<>), 401)]
        [ProducesResponseType(typeof(Response<>), 403)]
        [ProducesResponseType(500)]
        [Authorize(Policy = "CanReadOrder")]
        [Produces("application/json")]
        [HttpGet("{id}", Name = "GetOrder")]
        public async Task<ActionResult<OrderDto>> GetOrder(Guid id)
        {
            // add error handling
            var query = new GetOrder.OrderQuery(id);
            var queryResponse = await _mediator.Send(query);

            var response = new Response<OrderDto>(queryResponse);
            return Ok(response);
        }
        
        /// <summary>
        /// Creates a new Order record.
        /// </summary>
        /// <response code="201">Order created.</response>
        /// <response code="400">Order has missing/invalid values.</response>
        /// <response code="401">This request was not able to be authenticated.</response>
        /// <response code="403">The required permissions to access this resource were not present in the given request.</response>
        /// <response code="409">A record already exists with this primary key.</response>
        /// <response code="500">There was an error on the server while creating the Order.</response>
        [ProducesResponseType(typeof(Response<OrderDto>), 201)]
        [ProducesResponseType(typeof(Response<>), 400)]
        [ProducesResponseType(typeof(Response<>), 401)]
        [ProducesResponseType(typeof(Response<>), 403)]
        [ProducesResponseType(typeof(Response<>), 409)]
        [ProducesResponseType(500)]
        [Authorize(Policy = "CanAddOrder")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPost]
        public async Task<ActionResult<OrderDto>> AddOrder([FromBody]OrderForCreationDto orderForCreation)
        {
            // add error handling
            var command = new AddOrder.AddOrderCommand(orderForCreation);
            var commandResponse = await _mediator.Send(command);
            var response = new Response<OrderDto>(commandResponse);

            return CreatedAtRoute("GetOrder",
                new { commandResponse.Id },
                response);
        }
        
        /// <summary>
        /// Deletes an existing Order record.
        /// </summary>
        /// <response code="204">Order deleted.</response>
        /// <response code="400">Order has missing/invalid values.</response>
        /// <response code="500">There was an error on the server while creating the Order.</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(Response<>), 400)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(Guid id)
        {
            // add error handling
            var command = new DeleteOrder.DeleteOrderCommand(id);
            await _mediator.Send(command);

            return NoContent();
        }
        
        /// <summary>
        /// Updates an entire existing Order.
        /// </summary>
        /// <response code="204">Order updated.</response>
        /// <response code="400">Order has missing/invalid values.</response>
        /// <response code="401">This request was not able to be authenticated.</response>
        /// <response code="403">The required permissions to access this resource were not present in the given request.</response>
        /// <response code="500">There was an error on the server while creating the Order.</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(Response<>), 400)]
        [ProducesResponseType(typeof(Response<>), 401)]
        [ProducesResponseType(typeof(Response<>), 403)]
        [ProducesResponseType(500)]
        [Authorize(Policy = "CanUpdateOrder")]
        [Produces("application/json")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(Guid id, OrderForUpdateDto order)
        {
            // add error handling
            var command = new UpdateOrder.UpdateOrderCommand(id, order);
            await _mediator.Send(command);

            return NoContent();
        }
        
        /// <summary>
        /// Updates specific properties on an existing Order.
        /// </summary>
        /// <response code="204">Order updated.</response>
        /// <response code="400">Order has missing/invalid values.</response>
        /// <response code="401">This request was not able to be authenticated.</response>
        /// <response code="403">The required permissions to access this resource were not present in the given request.</response>
        /// <response code="500">There was an error on the server while creating the Order.</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(Response<>), 400)]
        [ProducesResponseType(typeof(Response<>), 401)]
        [ProducesResponseType(typeof(Response<>), 403)]
        [ProducesResponseType(500)]
        [Authorize(Policy = "CanUpdateOrder")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> PartiallyUpdateOrder(Guid id, JsonPatchDocument<OrderForUpdateDto> patchDoc)
        {
            // add error handling
            var command = new PatchOrder.PatchOrderCommand(id, patchDoc);
            await _mediator.Send(command);

            return NoContent();
        }
    }
}