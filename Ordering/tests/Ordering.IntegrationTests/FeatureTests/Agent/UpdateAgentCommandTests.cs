namespace Ordering.IntegrationTests.FeatureTests.Agent
{
    using Ordering.SharedTestHelpers.Fakes.Agent;
    using Ordering.IntegrationTests.TestUtilities;
    using Ordering.Core.Dtos.Agent;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.JsonPatch;
    using System.Linq;
    using Ordering.WebApi.Features.Agents;
    using static TestFixture;

    public class UpdateAgentCommandTests : TestBase
    {
        [Test]
        public async Task UpdateAgentCommand_Updates_Existing_Agent_In_Db()
        {
            // Arrange
            var fakeAgentOne = new FakeAgent { }.Generate();
            var updatedAgentDto = new FakeAgentForUpdateDto { }.Generate();
            await InsertAsync(fakeAgentOne);

            var agent = await ExecuteDbContextAsync(db => db.Agents.SingleOrDefaultAsync());
            var agentId = agent.AgentId;

            // Act
            var command = new UpdateAgent.UpdateAgentCommand(agentId, updatedAgentDto);
            await SendAsync(command);
            var updatedAgent = await ExecuteDbContextAsync(db => db.Agents.Where(a => a.AgentId == agentId).SingleOrDefaultAsync());

            // Assert
            updatedAgent.Should().BeEquivalentTo(updatedAgentDto, options =>
                options.ExcludingMissingMembers());
        }
    }
}