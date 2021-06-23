namespace NowProvisionAPI.FunctionalTests.FunctionalTests.Office
{
    using NowProvisionAPI.SharedTestHelpers.Fakes.Office;
    using NowProvisionAPI.Core.Dtos.Office;
    using NowProvisionAPI.FunctionalTests.TestUtilities;
    using Microsoft.AspNetCore.JsonPatch;
    using FluentAssertions;
    using NUnit.Framework;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class PartialOfficeUpdateTests : TestBase
    {
        [Test]
        public async Task Patch_Office_Returns_NoContent()
        {
            // Arrange
            var fakeOffice = new FakeOffice { }.Generate();
            var patchDoc = new JsonPatchDocument<OfficeForUpdateDto>();
            patchDoc.Replace(o => o.Name, "Easily Identified Value For Test");

            await InsertAsync(fakeOffice);

            // Act
            var route = ApiRoutes.Offices.Patch.Replace(ApiRoutes.Offices.OfficeId, fakeOffice.OfficeId.ToString());
            var result = await _client.PatchJsonRequestAsync(route, patchDoc);

            // Assert
            result.StatusCode.Should().Be(204);
        }
    }
}