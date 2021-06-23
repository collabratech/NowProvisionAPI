namespace NowProvisionAPI.IntegrationTests.FeatureTests.Agent
{
    using NowProvisionAPI.SharedTestHelpers.Fakes.Agent;
    using NowProvisionAPI.IntegrationTests.TestUtilities;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using System.Threading.Tasks;
    using NowProvisionAPI.WebApi.Features.Agents;
    using static TestFixture;
    using System;
    using NowProvisionAPI.Core.Exceptions;

    public class AddAgentCommandTests : TestBase
    {
        [Test]
        public async Task AddAgentCommand_Adds_New_Agent_To_Db()
        {
            // Arrange
            var fakeAgentOne = new FakeAgentForCreationDto { }.Generate();

            // Act
            var command = new AddAgent.AddAgentCommand(fakeAgentOne);
            var agentReturned = await SendAsync(command);
            var agentCreated = await ExecuteDbContextAsync(db => db.Agents.SingleOrDefaultAsync());

            // Assert
            agentReturned.Should().BeEquivalentTo(fakeAgentOne, options =>
                options.ExcludingMissingMembers());
            agentCreated.Should().BeEquivalentTo(fakeAgentOne, options =>
                options.ExcludingMissingMembers());
        }

        [Test]
        public async Task AddAgentCommand_Throws_Conflict_When_PK_Guid_Exists()
        {
            // Arrange
            var FakeAgent = new FakeAgent { }.Generate();
            var conflictRecord = new FakeAgentForCreationDto { }.Generate();
            conflictRecord.AgentId = FakeAgent.AgentId;

            await InsertAsync(FakeAgent);

            // Act
            var command = new AddAgent.AddAgentCommand(conflictRecord);
            Func<Task> act = () => SendAsync(command);

            // Assert
            act.Should().Throw<ConflictException>();
        }
    }
}