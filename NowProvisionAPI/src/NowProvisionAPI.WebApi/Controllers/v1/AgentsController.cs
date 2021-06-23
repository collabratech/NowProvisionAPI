namespace NowProvisionAPI.WebApi.Controllers.v1
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using System.Threading.Tasks;
    using NowProvisionAPI.Core.Dtos.Agent;
    using NowProvisionAPI.Core.Wrappers;
    using System.Threading;
    using MediatR;
    using NowProvisionAPI.WebApi.Features.Agents;

    [ApiController]
    [Route("api/Agents")]
    [ApiVersion("1.0")]
    public class AgentsController: ControllerBase
    {
        private readonly IMediator _mediator;

        public AgentsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        /// <summary>
        /// Gets a list of all Agents.
        /// </summary>
        /// <response code="200">Agent list returned successfully.</response>
        /// <response code="400">Agent has missing/invalid values.</response>
        /// <response code="500">There was an error on the server while creating the Agent.</response>
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
        [ProducesResponseType(typeof(Response<IEnumerable<AgentDto>>), 200)]
        [ProducesResponseType(typeof(Response<>), 400)]
        [ProducesResponseType(500)]
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpGet(Name = "GetAgents")]
        public async Task<IActionResult> GetAgents([FromQuery] AgentParametersDto agentParametersDto)
        {
            // add error handling
            var query = new GetAgentList.AgentListQuery(agentParametersDto);
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

            var response = new Response<IEnumerable<AgentDto>>(queryResponse);
            return Ok(response);
        }
        
        /// <summary>
        /// Gets a single Agent by ID.
        /// </summary>
        /// <response code="200">Agent record returned successfully.</response>
        /// <response code="400">Agent has missing/invalid values.</response>
        /// <response code="500">There was an error on the server while creating the Agent.</response>
        [ProducesResponseType(typeof(Response<AgentDto>), 200)]
        [ProducesResponseType(typeof(Response<>), 400)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        [HttpGet("{agentId}", Name = "GetAgent")]
        public async Task<ActionResult<AgentDto>> GetAgent(Guid agentId)
        {
            // add error handling
            var query = new GetAgent.AgentQuery(agentId);
            var queryResponse = await _mediator.Send(query);

            var response = new Response<AgentDto>(queryResponse);
            return Ok(response);
        }
        
        /// <summary>
        /// Creates a new Agent record.
        /// </summary>
        /// <response code="201">Agent created.</response>
        /// <response code="400">Agent has missing/invalid values.</response>
        /// <response code="409">A record already exists with this primary key.</response>
        /// <response code="500">There was an error on the server while creating the Agent.</response>
        [ProducesResponseType(typeof(Response<AgentDto>), 201)]
        [ProducesResponseType(typeof(Response<>), 400)]
        [ProducesResponseType(typeof(Response<>), 409)]
        [ProducesResponseType(500)]
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPost]
        public async Task<ActionResult<AgentDto>> AddAgent([FromBody]AgentForCreationDto agentForCreation)
        {
            // add error handling
            var command = new AddAgent.AddAgentCommand(agentForCreation);
            var commandResponse = await _mediator.Send(command);
            var response = new Response<AgentDto>(commandResponse);

            return CreatedAtRoute("GetAgent",
                new { commandResponse.AgentId },
                response);
        }
        
        /// <summary>
        /// Deletes an existing Agent record.
        /// </summary>
        /// <response code="204">Agent deleted.</response>
        /// <response code="400">Agent has missing/invalid values.</response>
        /// <response code="500">There was an error on the server while creating the Agent.</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(Response<>), 400)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        [HttpDelete("{agentId}")]
        public async Task<ActionResult> DeleteAgent(Guid agentId)
        {
            // add error handling
            var command = new DeleteAgent.DeleteAgentCommand(agentId);
            await _mediator.Send(command);

            return NoContent();
        }
        
        /// <summary>
        /// Updates an entire existing Agent.
        /// </summary>
        /// <response code="204">Agent updated.</response>
        /// <response code="400">Agent has missing/invalid values.</response>
        /// <response code="500">There was an error on the server while creating the Agent.</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(Response<>), 400)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        [HttpPut("{agentId}")]
        public async Task<IActionResult> UpdateAgent(Guid agentId, AgentForUpdateDto agent)
        {
            // add error handling
            var command = new UpdateAgent.UpdateAgentCommand(agentId, agent);
            await _mediator.Send(command);

            return NoContent();
        }
        
        /// <summary>
        /// Updates specific properties on an existing Agent.
        /// </summary>
        /// <response code="204">Agent updated.</response>
        /// <response code="400">Agent has missing/invalid values.</response>
        /// <response code="500">There was an error on the server while creating the Agent.</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(Response<>), 400)]
        [ProducesResponseType(500)]
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPatch("{agentId}")]
        public async Task<IActionResult> PartiallyUpdateAgent(Guid agentId, JsonPatchDocument<AgentForUpdateDto> patchDoc)
        {
            // add error handling
            var command = new PatchAgent.PatchAgentCommand(agentId, patchDoc);
            await _mediator.Send(command);

            return NoContent();
        }
    }
}