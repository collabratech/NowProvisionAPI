namespace NowProvisionAPI.FunctionalTests.FunctionalTests.NowProv
{
    using NowProvisionAPI.SharedTestHelpers.Fakes.NowProv;
    using NowProvisionAPI.Core.Dtos.NowProv;
    using NowProvisionAPI.FunctionalTests.TestUtilities;
    using Microsoft.AspNetCore.JsonPatch;
    using FluentAssertions;
    using NUnit.Framework;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class PartialNowProvUpdateTests : TestBase
    {
        [Test]
        public async Task Patch_NowProv_Returns_NoContent_WithAuth()
        {
            // Arrange
            var fakeNowProv = new FakeNowProv { }.Generate();
            var patchDoc = new JsonPatchDocument<NowProvForUpdateDto>();
            patchDoc.Replace(n => n.Events, "Easily Identified Value For Test");

            _client.AddAuth(new[] {"NowProv.update"});

            await InsertAsync(fakeNowProv);

            // Act
            var route = ApiRoutes.NowProvs.Patch.Replace(ApiRoutes.NowProvs.Id, fakeNowProv.Id.ToString());
            var result = await _client.PatchJsonRequestAsync(route, patchDoc);

            // Assert
            result.StatusCode.Should().Be(204);
        }
            
        [Test]
        public async Task Patch_NowProv_Returns_Unauthorized_Without_Valid_Token()
        {
            // Arrange
            var fakeNowProv = new FakeNowProv { }.Generate();
            var patchDoc = new JsonPatchDocument<NowProvForUpdateDto>();
            patchDoc.Replace(n => n.Events, "Easily Identified Value For Test");

            await InsertAsync(fakeNowProv);

            // Act
            var route = ApiRoutes.NowProvs.Patch.Replace(ApiRoutes.NowProvs.Id, fakeNowProv.Id.ToString());
            var result = await _client.PatchJsonRequestAsync(route, patchDoc);

            // Assert
            result.StatusCode.Should().Be(401);
        }
            
        [Test]
        public async Task Patch_NowProv_Returns_Forbidden_Without_Proper_Scope()
        {
            // Arrange
            var fakeNowProv = new FakeNowProv { }.Generate();
            var patchDoc = new JsonPatchDocument<NowProvForUpdateDto>();
            patchDoc.Replace(n => n.Events, "Easily Identified Value For Test");
            _client.AddAuth();

            await InsertAsync(fakeNowProv);

            // Act
            var route = ApiRoutes.NowProvs.Patch.Replace(ApiRoutes.NowProvs.Id, fakeNowProv.Id.ToString());
            var result = await _client.PatchJsonRequestAsync(route, patchDoc);

            // Assert
            result.StatusCode.Should().Be(403);
        }
    }
}