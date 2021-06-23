namespace Ordering.IntegrationTests.FeatureTests.Agent
{
    using Ordering.SharedTestHelpers.Fakes.Agent;
    using Ordering.IntegrationTests.TestUtilities;
    using Ordering.Core.Dtos.Agent;
    using Ordering.Core.Exceptions;
    using Ordering.WebApi.Features.Agents;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.JsonPatch;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using static TestFixture;

    public class PatchAgentCommandTests : TestBase
    {
        [Test]
        public async Task PatchAgentCommand_Updates_Existing_Agent_In_Db()
        {
            // Arrange
            var fakeAgentOne = new FakeAgent { }.Generate();
            await InsertAsync(fakeAgentOne);
            var agent = await ExecuteDbContextAsync(db => db.Agents.SingleOrDefaultAsync());
            var agentId = agent.AgentId;

            var patchDoc = new JsonPatchDocument<AgentForUpdateDto>();
            var newValue = "Easily Identified Value For Test";
            patchDoc.Replace(a => a.Name, newValue);

            // Act
            var command = new PatchAgent.PatchAgentCommand(agentId, patchDoc);
            await SendAsync(command);
            var updatedAgent = await ExecuteDbContextAsync(db => db.Agents.Where(a => a.AgentId == agentId).SingleOrDefaultAsync());

            // Assert
            updatedAgent.Name.Should().Be(newValue);
        }
        
        [Test]
        public async Task PatchAgentCommand_Throws_KeyNotFoundException_When_Bad_PK()
        {
            // Arrange
            var badId = Guid.NewGuid();
            var patchDoc = new JsonPatchDocument<AgentForUpdateDto>();

            // Act
            var command = new PatchAgent.PatchAgentCommand(badId, patchDoc);
            Func<Task> act = () => SendAsync(command);

            // Assert
            act.Should().Throw<KeyNotFoundException>();
        }

        [Test]
        public async Task PatchAgentCommand_Throws_ApiException_When_Null_Patchdoc()
        {
            // Arrange
            var randomId = Guid.NewGuid();

            // Act
            var command = new PatchAgent.PatchAgentCommand(randomId, null);
            Func<Task> act = () => SendAsync(command);

            // Assert
            act.Should().Throw<ApiException>();
        }
    }
}