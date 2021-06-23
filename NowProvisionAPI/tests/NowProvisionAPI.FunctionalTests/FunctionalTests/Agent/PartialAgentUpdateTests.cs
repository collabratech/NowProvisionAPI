namespace NowProvisionAPI.FunctionalTests.FunctionalTests.Agent
{
    using NowProvisionAPI.SharedTestHelpers.Fakes.Agent;
    using NowProvisionAPI.Core.Dtos.Agent;
    using NowProvisionAPI.FunctionalTests.TestUtilities;
    using Microsoft.AspNetCore.JsonPatch;
    using FluentAssertions;
    using NUnit.Framework;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class PartialAgentUpdateTests : TestBase
    {
        [Test]
        public async Task Patch_Agent_Returns_NoContent()
        {
            // Arrange
            var fakeAgent = new FakeAgent { }.Generate();
            var patchDoc = new JsonPatchDocument<AgentForUpdateDto>();
            patchDoc.Replace(a => a.Name, "Easily Identified Value For Test");

            await InsertAsync(fakeAgent);

            // Act
            var route = ApiRoutes.Agents.Patch.Replace(ApiRoutes.Agents.AgentId, fakeAgent.AgentId.ToString());
            var result = await _client.PatchJsonRequestAsync(route, patchDoc);

            // Assert
            result.StatusCode.Should().Be(204);
        }
    }
}