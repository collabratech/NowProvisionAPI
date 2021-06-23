namespace Ordering.WebApi.Controllers.v1
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using System.Threading.Tasks;
    using Ordering.Core.Dtos.Office;
    using Ordering.Core.Wrappers;
    using System.Threading;
    using MediatR;
    using Ordering.WebApi.Features.Offices;

    [ApiController]
    [Route("api/Offices")]
    [ApiVersion("1.0")]
    public class OfficesController: ControllerBase
    {
        private readonly IMediator _mediator;

        public OfficesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        /// <summary>
        /// Gets a list of all Offices.
        /// </summary>
        /// <response code="200">Office list returned successfully.</response>
        /// <response code="400">Office has missing/invalid values.</response>
        /// <response code="500">There was an error on the server while creating the Office.</response>
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
        [ProducesResponseType(typeof(Response<IEnumerable<OfficeDto>>), 200)]
        [ProducesResponseType(typeof(Response<>), 400)]
        [ProducesResponseType(500)]
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpGet(Name = "GetOffices")]
        public async Task<IActionResult> GetOffices([FromQuery] OfficeParametersDto officeParametersDto)
        {
            // add error handling
            var query = new GetOfficeList.OfficeListQuery(officeParametersDto);
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

            var response = new Response<IEnumerable<OfficeDto>>(queryResponse);
            return Ok(response);
        }
        
        /// <summary>
        /// Gets a single Office by ID.
        /// </summary>
        /// <response code="200">Office record returned successfully.</response>
        /// <response code="400">Office has missing/invalid values.</response>
        /// <response code="500">There was an error on the server while creating the Office.</response>
        [ProducesResponseType(typeof(Response<OfficeDto>), 200)]
        [ProducesResponseType(typeof(Response<>), 400)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        [HttpGet("{officeId}", Name = "GetOffice")]
        public async Task<ActionResult<OfficeDto>> GetOffice(Guid officeId)
        {
            // add error handling
            var query = new GetOffice.OfficeQuery(officeId);
            var queryResponse = await _mediator.Send(query);

            var response = new Response<OfficeDto>(queryResponse);
            return Ok(response);
        }
        
        /// <summary>
        /// Creates a new Office record.
        /// </summary>
        /// <response code="201">Office created.</response>
        /// <response code="400">Office has missing/invalid values.</response>
        /// <response code="409">A record already exists with this primary key.</response>
        /// <response code="500">There was an error on the server while creating the Office.</response>
        [ProducesResponseType(typeof(Response<OfficeDto>), 201)]
        [ProducesResponseType(typeof(Response<>), 400)]
        [ProducesResponseType(typeof(Response<>), 409)]
        [ProducesResponseType(500)]
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPost]
        public async Task<ActionResult<OfficeDto>> AddOffice([FromBody]OfficeForCreationDto officeForCreation)
        {
            // add error handling
            var command = new AddOffice.AddOfficeCommand(officeForCreation);
            var commandResponse = await _mediator.Send(command);
            var response = new Response<OfficeDto>(commandResponse);

            return CreatedAtRoute("GetOffice",
                new { commandResponse.OfficeId },
                response);
        }
        
        /// <summary>
        /// Deletes an existing Office record.
        /// </summary>
        /// <response code="204">Office deleted.</response>
        /// <response code="400">Office has missing/invalid values.</response>
        /// <response code="500">There was an error on the server while creating the Office.</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(Response<>), 400)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        [HttpDelete("{officeId}")]
        public async Task<ActionResult> DeleteOffice(Guid officeId)
        {
            // add error handling
            var command = new DeleteOffice.DeleteOfficeCommand(officeId);
            await _mediator.Send(command);

            return NoContent();
        }
        
        /// <summary>
        /// Updates an entire existing Office.
        /// </summary>
        /// <response code="204">Office updated.</response>
        /// <response code="400">Office has missing/invalid values.</response>
        /// <response code="500">There was an error on the server while creating the Office.</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(Response<>), 400)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        [HttpPut("{officeId}")]
        public async Task<IActionResult> UpdateOffice(Guid officeId, OfficeForUpdateDto office)
        {
            // add error handling
            var command = new UpdateOffice.UpdateOfficeCommand(officeId, office);
            await _mediator.Send(command);

            return NoContent();
        }
        
        /// <summary>
        /// Updates specific properties on an existing Office.
        /// </summary>
        /// <response code="204">Office updated.</response>
        /// <response code="400">Office has missing/invalid values.</response>
        /// <response code="500">There was an error on the server while creating the Office.</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(Response<>), 400)]
        [ProducesResponseType(500)]
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPatch("{officeId}")]
        public async Task<IActionResult> PartiallyUpdateOffice(Guid officeId, JsonPatchDocument<OfficeForUpdateDto> patchDoc)
        {
            // add error handling
            var command = new PatchOffice.PatchOfficeCommand(officeId, patchDoc);
            await _mediator.Send(command);

            return NoContent();
        }
    }
}