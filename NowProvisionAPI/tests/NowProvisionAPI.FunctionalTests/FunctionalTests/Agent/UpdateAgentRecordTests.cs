namespace NowProvisionAPI.FunctionalTests.FunctionalTests.Agent
{
    using NowProvisionAPI.SharedTestHelpers.Fakes.Agent;
    using NowProvisionAPI.FunctionalTests.TestUtilities;
    using FluentAssertions;
    using NUnit.Framework;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class UpdateAgentRecordTests : TestBase
    {
        [Test]
        public async Task Put_Agent_Returns_NoContent()
        {
            // Arrange
            var fakeAgent = new FakeAgent { }.Generate();
            var updatedAgentDto = new FakeAgentForUpdateDto { }.Generate();

            await InsertAsync(fakeAgent);

            // Act
            var route = ApiRoutes.Agents.Put.Replace(ApiRoutes.Agents.AgentId, fakeAgent.AgentId.ToString());
            var result = await _client.PutJsonRequestAsync(route, updatedAgentDto);

            // Assert
            result.StatusCode.Should().Be(204);
        }
    }
}