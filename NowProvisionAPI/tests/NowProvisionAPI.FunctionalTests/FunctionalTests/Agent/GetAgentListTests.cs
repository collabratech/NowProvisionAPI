namespace NowProvisionAPI.FunctionalTests.FunctionalTests.Agent
{
    using NowProvisionAPI.SharedTestHelpers.Fakes.Agent;
    using NowProvisionAPI.FunctionalTests.TestUtilities;
    using FluentAssertions;
    using NUnit.Framework;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class GetAgentListTests : TestBase
    {
        [Test]
        public async Task Get_Agent_List_Returns_NoContent()
        {
            // Arrange
            // N/A

            // Act
            var result = await _client.GetRequestAsync(ApiRoutes.Agents.GetList);

            // Assert
            result.StatusCode.Should().Be(200);
        }
    }
}