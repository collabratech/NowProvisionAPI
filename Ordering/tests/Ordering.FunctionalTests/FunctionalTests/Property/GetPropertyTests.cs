namespace Ordering.FunctionalTests.FunctionalTests.Property
{
    using Ordering.SharedTestHelpers.Fakes.Property;
    using Ordering.FunctionalTests.TestUtilities;
    using FluentAssertions;
    using NUnit.Framework;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class GetPropertyTests : TestBase
    {
        [Test]
        public async Task Get_Property_Record_Returns_NoContent()
        {
            // Arrange
            var fakeProperty = new FakeProperty { }.Generate();

            await InsertAsync(fakeProperty);

            // Act
            var route = ApiRoutes.Propertys.GetRecord.Replace(ApiRoutes.Propertys.PropertyId, fakeProperty.PropertyId.ToString());
            var result = await _client.GetRequestAsync(route);

            // Assert
            result.StatusCode.Should().Be(200);
        }
    }
}