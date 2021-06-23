namespace Ordering.FunctionalTests.FunctionalTests.Agent
{
    using Ordering.SharedTestHelpers.Fakes.Agent;
    using Ordering.FunctionalTests.TestUtilities;
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