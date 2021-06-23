namespace NowProvisionAPI.FunctionalTests.FunctionalTests.NowProv
{
    using NowProvisionAPI.SharedTestHelpers.Fakes.NowProv;
    using NowProvisionAPI.FunctionalTests.TestUtilities;
    using FluentAssertions;
    using NUnit.Framework;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class DeleteNowProvTests : TestBase
    {
        [Test]
        public async Task Delete_NowProvReturns_NoContent()
        {
            // Arrange
            var fakeNowProv = new FakeNowProv { }.Generate();

            await InsertAsync(fakeNowProv);

            // Act
            var route = ApiRoutes.NowProvs.Delete.Replace(ApiRoutes.NowProvs.Id, fakeNowProv.Id.ToString());
            var result = await _client.DeleteRequestAsync(route);

            // Assert
            result.StatusCode.Should().Be(204);
        }
    }
}