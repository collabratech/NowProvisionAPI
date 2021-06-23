namespace NowProvisionAPI.FunctionalTests.FunctionalTests.Office
{
    using NowProvisionAPI.SharedTestHelpers.Fakes.Office;
    using NowProvisionAPI.FunctionalTests.TestUtilities;
    using FluentAssertions;
    using NUnit.Framework;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class GetOfficeListTests : TestBase
    {
        [Test]
        public async Task Get_Office_List_Returns_NoContent()
        {
            // Arrange
            // N/A

            // Act
            var result = await _client.GetRequestAsync(ApiRoutes.Offices.GetList);

            // Assert
            result.StatusCode.Should().Be(200);
        }
    }
}