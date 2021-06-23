namespace Ordering.IntegrationTests.FeatureTests.Agent
{
    using Ordering.SharedTestHelpers.Fakes.Agent;
    using Ordering.IntegrationTests.TestUtilities;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Ordering.WebApi.Features.Agents;
    using static TestFixture;

    public class AgentQueryTests : TestBase
    {
        [Test]
        public async Task AgentQuery_Returns_Resource_With_Accurate_Props()
        {
            // Arrange
            var fakeAgentOne = new FakeAgent { }.Generate();
            await InsertAsync(fakeAgentOne);

            // Act
            var query = new GetAgent.AgentQuery(fakeAgentOne.AgentId);
            var agents = await SendAsync(query);

            // Assert
            agents.Should().BeEquivalentTo(fakeAgentOne, options =>
                options.ExcludingMissingMembers());
        }

        [Test]
        public async Task AgentQuery_Throws_KeyNotFoundException_When_Record_Does_Not_Exist()
        {
            // Arrange
            var badId = Guid.NewGuid();

            // Act
            var query = new GetAgent.AgentQuery(badId);
            Func<Task> act = () => SendAsync(query);

            // Assert
            act.Should().Throw<KeyNotFoundException>();
        }
    }
}