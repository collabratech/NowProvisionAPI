namespace Ordering.WebApi.Controllers.v1
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using System.Threading.Tasks;
    using Ordering.Core.Dtos.Property;
    using Ordering.Core.Wrappers;
    using System.Threading;
    using MediatR;
    using Ordering.WebApi.Features.Propertys;

    [ApiController]
    [Route("api/Propertys")]
    [ApiVersion("1.0")]
    public class PropertysController: ControllerBase
    {
        private readonly IMediator _mediator;

        public PropertysController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        /// <summary>
        /// Gets a list of all Propertys.
        /// </summary>
        /// <response code="200">Property list returned successfully.</response>
        /// <response code="400">Property has missing/invalid values.</response>
        /// <response code="500">There was an error on the server while creating the Property.</response>
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
        [ProducesResponseType(typeof(Response<IEnumerable<PropertyDto>>), 200)]
        [ProducesResponseType(typeof(Response<>), 400)]
        [ProducesResponseType(500)]
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpGet(Name = "GetPropertys")]
        public async Task<IActionResult> GetPropertys([FromQuery] PropertyParametersDto propertyParametersDto)
        {
            // add error handling
            var query = new GetPropertyList.PropertyListQuery(propertyParametersDto);
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

            var response = new Response<IEnumerable<PropertyDto>>(queryResponse);
            return Ok(response);
        }
        
        /// <summary>
        /// Gets a single Property by ID.
        /// </summary>
        /// <response code="200">Property record returned successfully.</response>
        /// <response code="400">Property has missing/invalid values.</response>
        /// <response code="500">There was an error on the server while creating the Property.</response>
        [ProducesResponseType(typeof(Response<PropertyDto>), 200)]
        [ProducesResponseType(typeof(Response<>), 400)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        [HttpGet("{propertyId}", Name = "GetProperty")]
        public async Task<ActionResult<PropertyDto>> GetProperty(Guid propertyId)
        {
            // add error handling
            var query = new GetProperty.PropertyQuery(propertyId);
            var queryResponse = await _mediator.Send(query);

            var response = new Response<PropertyDto>(queryResponse);
            return Ok(response);
        }
        
        /// <summary>
        /// Creates a new Property record.
        /// </summary>
        /// <response code="201">Property created.</response>
        /// <response code="400">Property has missing/invalid values.</response>
        /// <response code="409">A record already exists with this primary key.</response>
        /// <response code="500">There was an error on the server while creating the Property.</response>
        [ProducesResponseType(typeof(Response<PropertyDto>), 201)]
        [ProducesResponseType(typeof(Response<>), 400)]
        [ProducesResponseType(typeof(Response<>), 409)]
        [ProducesResponseType(500)]
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPost]
        public async Task<ActionResult<PropertyDto>> AddProperty([FromBody]PropertyForCreationDto propertyForCreation)
        {
            // add error handling
            var command = new AddProperty.AddPropertyCommand(propertyForCreation);
            var commandResponse = await _mediator.Send(command);
            var response = new Response<PropertyDto>(commandResponse);

            return CreatedAtRoute("GetProperty",
                new { commandResponse.PropertyId },
                response);
        }
        
        /// <summary>
        /// Deletes an existing Property record.
        /// </summary>
        /// <response code="204">Property deleted.</response>
        /// <response code="400">Property has missing/invalid values.</response>
        /// <response code="500">There was an error on the server while creating the Property.</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(Response<>), 400)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        [HttpDelete("{propertyId}")]
        public async Task<ActionResult> DeleteProperty(Guid propertyId)
        {
            // add error handling
            var command = new DeleteProperty.DeletePropertyCommand(propertyId);
            await _mediator.Send(command);

            return NoContent();
        }
        
        /// <summary>
        /// Updates an entire existing Property.
        /// </summary>
        /// <response code="204">Property updated.</response>
        /// <response code="400">Property has missing/invalid values.</response>
        /// <response code="500">There was an error on the server while creating the Property.</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(Response<>), 400)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        [HttpPut("{propertyId}")]
        public async Task<IActionResult> UpdateProperty(Guid propertyId, PropertyForUpdateDto property)
        {
            // add error handling
            var command = new UpdateProperty.UpdatePropertyCommand(propertyId, property);
            await _mediator.Send(command);

            return NoContent();
        }
        
        /// <summary>
        /// Updates specific properties on an existing Property.
        /// </summary>
        /// <response code="204">Property updated.</response>
        /// <response code="400">Property has missing/invalid values.</response>
        /// <response code="500">There was an error on the server while creating the Property.</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(Response<>), 400)]
        [ProducesResponseType(500)]
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPatch("{propertyId}")]
        public async Task<IActionResult> PartiallyUpdateProperty(Guid propertyId, JsonPatchDocument<PropertyForUpdateDto> patchDoc)
        {
            // add error handling
            var command = new PatchProperty.PatchPropertyCommand(propertyId, patchDoc);
            await _mediator.Send(command);

            return NoContent();
        }
    }
}