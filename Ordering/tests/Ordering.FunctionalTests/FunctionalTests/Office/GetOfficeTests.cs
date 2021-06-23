namespace Ordering.FunctionalTests.FunctionalTests.Office
{
    using Ordering.SharedTestHelpers.Fakes.Office;
    using Ordering.FunctionalTests.TestUtilities;
    using FluentAssertions;
    using NUnit.Framework;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class GetOfficeTests : TestBase
    {
        [Test]
        public async Task Get_Office_Record_Returns_NoContent()
        {
            // Arrange
            var fakeOffice = new FakeOffice { }.Generate();

            await InsertAsync(fakeOffice);

            // Act
            var route = ApiRoutes.Offices.GetRecord.Replace(ApiRoutes.Offices.OfficeId, fakeOffice.OfficeId.ToString());
            var result = await _client.GetRequestAsync(route);

            // Assert
            result.StatusCode.Should().Be(200);
        }
    }
}