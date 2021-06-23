namespace Ordering.FunctionalTests.FunctionalTests.Agent
{
    using Ordering.SharedTestHelpers.Fakes.Agent;
    using Ordering.FunctionalTests.TestUtilities;
    using FluentAssertions;
    using NUnit.Framework;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class DeleteAgentTests : TestBase
    {
        [Test]
        public async Task Delete_AgentReturns_NoContent()
        {
            // Arrange
            var fakeAgent = new FakeAgent { }.Generate();

            await InsertAsync(fakeAgent);

            // Act
            var route = ApiRoutes.Agents.Delete.Replace(ApiRoutes.Agents.AgentId, fakeAgent.AgentId.ToString());
            var result = await _client.DeleteRequestAsync(route);

            // Assert
            result.StatusCode.Should().Be(204);
        }
    }
}