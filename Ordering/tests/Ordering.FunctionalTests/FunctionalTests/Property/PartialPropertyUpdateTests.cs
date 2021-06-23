namespace Ordering.FunctionalTests.FunctionalTests.Property
{
    using Ordering.SharedTestHelpers.Fakes.Property;
    using Ordering.Core.Dtos.Property;
    using Ordering.FunctionalTests.TestUtilities;
    using Microsoft.AspNetCore.JsonPatch;
    using FluentAssertions;
    using NUnit.Framework;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class PartialPropertyUpdateTests : TestBase
    {
        [Test]
        public async Task Patch_Property_Returns_NoContent()
        {
            // Arrange
            var fakeProperty = new FakeProperty { }.Generate();
            var patchDoc = new JsonPatchDocument<PropertyForUpdateDto>();
            patchDoc.Replace(p => p.Slug, "Easily Identified Value For Test");

            await InsertAsync(fakeProperty);

            // Act
            var route = ApiRoutes.Propertys.Patch.Replace(ApiRoutes.Propertys.PropertyId, fakeProperty.PropertyId.ToString());
            var result = await _client.PatchJsonRequestAsync(route, patchDoc);

            // Assert
            result.StatusCode.Should().Be(204);
        }
    }
}