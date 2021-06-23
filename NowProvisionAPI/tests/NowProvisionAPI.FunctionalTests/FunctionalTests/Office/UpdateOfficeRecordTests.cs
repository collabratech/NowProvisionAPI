namespace NowProvisionAPI.FunctionalTests.FunctionalTests.Office
{
    using NowProvisionAPI.SharedTestHelpers.Fakes.Office;
    using NowProvisionAPI.FunctionalTests.TestUtilities;
    using FluentAssertions;
    using NUnit.Framework;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class UpdateOfficeRecordTests : TestBase
    {
        [Test]
        public async Task Put_Office_Returns_NoContent()
        {
            // Arrange
            var fakeOffice = new FakeOffice { }.Generate();
            var updatedOfficeDto = new FakeOfficeForUpdateDto { }.Generate();

            await InsertAsync(fakeOffice);

            // Act
            var route = ApiRoutes.Offices.Put.Replace(ApiRoutes.Offices.OfficeId, fakeOffice.OfficeId.ToString());
            var result = await _client.PutJsonRequestAsync(route, updatedOfficeDto);

            // Assert
            result.StatusCode.Should().Be(204);
        }
    }
}