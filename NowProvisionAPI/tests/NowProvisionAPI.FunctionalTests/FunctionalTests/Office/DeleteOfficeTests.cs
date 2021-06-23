namespace NowProvisionAPI.FunctionalTests.FunctionalTests.Office
{
    using NowProvisionAPI.SharedTestHelpers.Fakes.Office;
    using NowProvisionAPI.FunctionalTests.TestUtilities;
    using FluentAssertions;
    using NUnit.Framework;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class DeleteOfficeTests : TestBase
    {
        [Test]
        public async Task Delete_OfficeReturns_NoContent()
        {
            // Arrange
            var fakeOffice = new FakeOffice { }.Generate();

            await InsertAsync(fakeOffice);

            // Act
            var route = ApiRoutes.Offices.Delete.Replace(ApiRoutes.Offices.OfficeId, fakeOffice.OfficeId.ToString());
            var result = await _client.DeleteRequestAsync(route);

            // Assert
            result.StatusCode.Should().Be(204);
        }
    }
}