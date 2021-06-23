namespace Ordering.IntegrationTests.FeatureTests.Agent
{
    using Ordering.SharedTestHelpers.Fakes.Agent;
    using Ordering.IntegrationTests.TestUtilities;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System;
    using System.Threading.Tasks;
    using Ordering.WebApi.Features.Agents;
    using static TestFixture;

    public class DeleteAgentCommandTests : TestBase
    {
        [Test]
        public async Task DeleteAgentCommand_Deletes_Agent_From_Db()
        {
            // Arrange
            var fakeAgentOne = new FakeAgent { }.Generate();
            await InsertAsync(fakeAgentOne);
            var agent = await ExecuteDbContextAsync(db => db.Agents.SingleOrDefaultAsync());
            var agentId = agent.AgentId;

            // Act
            var command = new DeleteAgent.DeleteAgentCommand(agentId);
            await SendAsync(command);
            var agents = await ExecuteDbContextAsync(db => db.Agents.ToListAsync());

            // Assert
            agents.Count.Should().Be(0);
        }

        [Test]
        public async Task DeleteAgentCommand_Throws_KeyNotFoundException_When_Record_Does_Not_Exist()
        {
            // Arrange
            var badId = Guid.NewGuid();

            // Act
            var command = new DeleteAgent.DeleteAgentCommand(badId);
            Func<Task> act = () => SendAsync(command);

            // Assert
            act.Should().Throw<KeyNotFoundException>();
        }
    }
}