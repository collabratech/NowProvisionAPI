namespace NowProvisionAPI.FunctionalTests.FunctionalTests.Property
{
    using NowProvisionAPI.SharedTestHelpers.Fakes.Property;
    using NowProvisionAPI.FunctionalTests.TestUtilities;
    using FluentAssertions;
    using NUnit.Framework;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class GetPropertyListTests : TestBase
    {
        [Test]
        public async Task Get_Property_List_Returns_NoContent()
        {
            // Arrange
            // N/A

            // Act
            var result = await _client.GetRequestAsync(ApiRoutes.Propertys.GetList);

            // Assert
            result.StatusCode.Should().Be(200);
        }
    }
}