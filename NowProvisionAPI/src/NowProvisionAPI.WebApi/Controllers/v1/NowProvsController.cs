namespace NowProvisionAPI.WebApi.Controllers.v1
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using System.Threading.Tasks;
    using NowProvisionAPI.Core.Dtos.NowProv;
    using NowProvisionAPI.Core.Wrappers;
    using System.Threading;
    using MediatR;
    using NowProvisionAPI.WebApi.Features.NowProvs;

    [ApiController]
    [Route("api/NowProvs")]
    [ApiVersion("1.0")]
    public class NowProvsController: ControllerBase
    {
        private readonly IMediator _mediator;

        public NowProvsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        /// <summary>
        /// Gets a list of all NowProvs.
        /// </summary>
        /// <response code="200">NowProv list returned successfully.</response>
        /// <response code="400">NowProv has missing/invalid values.</response>
        /// <response code="401">This request was not able to be authenticated.</response>
        /// <response code="403">The required permissions to access this resource were not present in the given request.</response>
        /// <response code="500">There was an error on the server while creating the NowProv.</response>
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
        [ProducesResponseType(typeof(Response<IEnumerable<NowProvDto>>), 200)]
        [ProducesResponseType(typeof(Response<>), 400)]
        [ProducesResponseType(typeof(Response<>), 401)]
        [ProducesResponseType(typeof(Response<>), 403)]
        [ProducesResponseType(500)]
        //[Authorize(Policy = "CanReadNowProv")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpGet(Name = "GetNowProvs")]
        public async Task<IActionResult> GetNowProvs([FromQuery] NowProvParametersDto nowProvParametersDto)
        {
            // add error handling
            var query = new GetNowProvList.NowProvListQuery(nowProvParametersDto);
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

            var response = new Response<IEnumerable<NowProvDto>>(queryResponse);
            return Ok(response);
        }
        
        /// <summary>
        /// Gets a single NowProv by ID.
        /// </summary>
        /// <response code="200">NowProv record returned successfully.</response>
        /// <response code="400">NowProv has missing/invalid values.</response>
        /// <response code="401">This request was not able to be authenticated.</response>
        /// <response code="403">The required permissions to access this resource were not present in the given request.</response>
        /// <response code="500">There was an error on the server while creating the NowProv.</response>
        [ProducesResponseType(typeof(Response<NowProvDto>), 200)]
        [ProducesResponseType(typeof(Response<>), 400)]
        [ProducesResponseType(typeof(Response<>), 401)]
        [ProducesResponseType(typeof(Response<>), 403)]
        [ProducesResponseType(500)]
        //[Authorize(Policy = "CanReadNowProv")]
        [Produces("application/json")]
        [HttpGet("{id}", Name = "GetNowProv")]
        public async Task<ActionResult<NowProvDto>> GetNowProv(Guid id)
        {
            // add error handling
            var query = new GetNowProv.NowProvQuery(id);
            var queryResponse = await _mediator.Send(query);

            var response = new Response<NowProvDto>(queryResponse);
            return Ok(response);
        }
        
        /// <summary>
        /// Creates a new NowProv record.
        /// </summary>
        /// <response code="201">NowProv created.</response>
        /// <response code="400">NowProv has missing/invalid values.</response>
        /// <response code="401">This request was not able to be authenticated.</response>
        /// <response code="403">The required permissions to access this resource were not present in the given request.</response>
        /// <response code="409">A record already exists with this primary key.</response>
        /// <response code="500">There was an error on the server while creating the NowProv.</response>
        [ProducesResponseType(typeof(Response<NowProvDto>), 201)]
        [ProducesResponseType(typeof(Response<>), 400)]
        [ProducesResponseType(typeof(Response<>), 401)]
        [ProducesResponseType(typeof(Response<>), 403)]
        [ProducesResponseType(typeof(Response<>), 409)]
        [ProducesResponseType(500)]
        [Authorize(Policy = "CanAddNowProv")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPost]
        public async Task<ActionResult<NowProvDto>> AddNowProv([FromBody]NowProvForCreationDto nowProvForCreation)
        {
            // add error handling
            var command = new AddNowProv.AddNowProvCommand(nowProvForCreation);
            var commandResponse = await _mediator.Send(command);
            var response = new Response<NowProvDto>(commandResponse);

            return CreatedAtRoute("GetNowProv",
                new { commandResponse.Id },
                response);
        }
        
        /// <summary>
        /// Deletes an existing NowProv record.
        /// </summary>
        /// <response code="204">NowProv deleted.</response>
        /// <response code="400">NowProv has missing/invalid values.</response>
        /// <response code="500">There was an error on the server while creating the NowProv.</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(Response<>), 400)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteNowProv(Guid id)
        {
            // add error handling
            var command = new DeleteNowProv.DeleteNowProvCommand(id);
            await _mediator.Send(command);

            return NoContent();
        }
        
        /// <summary>
        /// Updates an entire existing NowProv.
        /// </summary>
        /// <response code="204">NowProv updated.</response>
        /// <response code="400">NowProv has missing/invalid values.</response>
        /// <response code="401">This request was not able to be authenticated.</response>
        /// <response code="403">The required permissions to access this resource were not present in the given request.</response>
        /// <response code="500">There was an error on the server while creating the NowProv.</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(Response<>), 400)]
        [ProducesResponseType(typeof(Response<>), 401)]
        [ProducesResponseType(typeof(Response<>), 403)]
        [ProducesResponseType(500)]
        [Authorize(Policy = "CanUpdateNowProv")]
        [Produces("application/json")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNowProv(Guid id, NowProvForUpdateDto nowProv)
        {
            // add error handling
            var command = new UpdateNowProv.UpdateNowProvCommand(id, nowProv);
            await _mediator.Send(command);

            return NoContent();
        }
        
        /// <summary>
        /// Updates specific properties on an existing NowProv.
        /// </summary>
        /// <response code="204">NowProv updated.</response>
        /// <response code="400">NowProv has missing/invalid values.</response>
        /// <response code="401">This request was not able to be authenticated.</response>
        /// <response code="403">The required permissions to access this resource were not present in the given request.</response>
        /// <response code="500">There was an error on the server while creating the NowProv.</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(Response<>), 400)]
        [ProducesResponseType(typeof(Response<>), 401)]
        [ProducesResponseType(typeof(Response<>), 403)]
        [ProducesResponseType(500)]
        [Authorize(Policy = "CanUpdateNowProv")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> PartiallyUpdateNowProv(Guid id, JsonPatchDocument<NowProvForUpdateDto> patchDoc)
        {
            // add error handling
            var command = new PatchNowProv.PatchNowProvCommand(id, patchDoc);
            await _mediator.Send(command);

            return NoContent();
        }
    }
}