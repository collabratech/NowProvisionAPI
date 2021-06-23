namespace Ordering.FunctionalTests.FunctionalTests.Property
{
    using Ordering.SharedTestHelpers.Fakes.Property;
    using Ordering.FunctionalTests.TestUtilities;
    using FluentAssertions;
    using NUnit.Framework;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class UpdatePropertyRecordTests : TestBase
    {
        [Test]
        public async Task Put_Property_Returns_NoContent()
        {
            // Arrange
            var fakeProperty = new FakeProperty { }.Generate();
            var updatedPropertyDto = new FakePropertyForUpdateDto { }.Generate();

            await InsertAsync(fakeProperty);

            // Act
            var route = ApiRoutes.Propertys.Put.Replace(ApiRoutes.Propertys.PropertyId, fakeProperty.PropertyId.ToString());
            var result = await _client.PutJsonRequestAsync(route, updatedPropertyDto);

            // Assert
            result.StatusCode.Should().Be(204);
        }
    }
}