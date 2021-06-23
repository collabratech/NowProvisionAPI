namespace Ordering.FunctionalTests.FunctionalTests.Agent
{
    using Ordering.SharedTestHelpers.Fakes.Agent;
    using Ordering.FunctionalTests.TestUtilities;
    using FluentAssertions;
    using NUnit.Framework;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class GetAgentTests : TestBase
    {
        [Test]
        public async Task Get_Agent_Record_Returns_NoContent()
        {
            // Arrange
            var fakeAgent = new FakeAgent { }.Generate();

            await InsertAsync(fakeAgent);

            // Act
            var route = ApiRoutes.Agents.GetRecord.Replace(ApiRoutes.Agents.AgentId, fakeAgent.AgentId.ToString());
            var result = await _client.GetRequestAsync(route);

            // Assert
            result.StatusCode.Should().Be(200);
        }
    }
}