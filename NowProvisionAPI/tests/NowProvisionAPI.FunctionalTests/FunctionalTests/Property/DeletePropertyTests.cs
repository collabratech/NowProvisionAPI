namespace NowProvisionAPI.FunctionalTests.FunctionalTests.Property
{
    using NowProvisionAPI.SharedTestHelpers.Fakes.Property;
    using NowProvisionAPI.FunctionalTests.TestUtilities;
    using FluentAssertions;
    using NUnit.Framework;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class DeletePropertyTests : TestBase
    {
        [Test]
        public async Task Delete_PropertyReturns_NoContent()
        {
            // Arrange
            var fakeProperty = new FakeProperty { }.Generate();

            await InsertAsync(fakeProperty);

            // Act
            var route = ApiRoutes.Propertys.Delete.Replace(ApiRoutes.Propertys.PropertyId, fakeProperty.PropertyId.ToString());
            var result = await _client.DeleteRequestAsync(route);

            // Assert
            result.StatusCode.Should().Be(204);
        }
    }
}