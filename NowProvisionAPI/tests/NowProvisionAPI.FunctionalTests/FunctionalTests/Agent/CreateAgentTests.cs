namespace NowProvisionAPI.FunctionalTests.FunctionalTests.Agent
{
    using NowProvisionAPI.SharedTestHelpers.Fakes.Agent;
    using NowProvisionAPI.FunctionalTests.TestUtilities;
    using FluentAssertions;
    using NUnit.Framework;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class CreateAgentTests : TestBase
    {
        [Test]
        public async Task Create_Agent_Returns_Created()
        {
            // Arrange
            var fakeAgent = new FakeAgentForCreationDto { }.Generate();

            // Act
            var route = ApiRoutes.Agents.Create;
            var result = await _client.PostJsonRequestAsync(route, fakeAgent);

            // Assert
            result.StatusCode.Should().Be(201);
        }
    }
}