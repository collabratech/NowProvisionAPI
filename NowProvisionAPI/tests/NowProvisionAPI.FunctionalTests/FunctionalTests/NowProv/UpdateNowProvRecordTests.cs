namespace NowProvisionAPI.FunctionalTests.FunctionalTests.NowProv
{
    using NowProvisionAPI.SharedTestHelpers.Fakes.NowProv;
    using NowProvisionAPI.FunctionalTests.TestUtilities;
    using FluentAssertions;
    using NUnit.Framework;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class UpdateNowProvRecordTests : TestBase
    {
        [Test]
        public async Task Put_NowProv_Returns_NoContent_WithAuth()
        {
            // Arrange
            var fakeNowProv = new FakeNowProv { }.Generate();
            var updatedNowProvDto = new FakeNowProvForUpdateDto { }.Generate();

            _client.AddAuth(new[] {"NowProv.update"});

            await InsertAsync(fakeNowProv);

            // Act
            var route = ApiRoutes.NowProvs.Put.Replace(ApiRoutes.NowProvs.Id, fakeNowProv.Id.ToString());
            var result = await _client.PutJsonRequestAsync(route, updatedNowProvDto);

            // Assert
            result.StatusCode.Should().Be(204);
        }
            
        [Test]
        public async Task Put_NowProv_Returns_Unauthorized_Without_Valid_Token()
        {
            // Arrange
            var fakeNowProv = new FakeNowProv { }.Generate();
            var updatedNowProvDto = new FakeNowProvForUpdateDto { }.Generate();

            await InsertAsync(fakeNowProv);

            // Act
            var route = ApiRoutes.NowProvs.Put.Replace(ApiRoutes.NowProvs.Id, fakeNowProv.Id.ToString());
            var result = await _client.PutJsonRequestAsync(route, updatedNowProvDto);

            // Assert
            result.StatusCode.Should().Be(401);
        }
            
        [Test]
        public async Task Put_NowProv_Returns_Forbidden_Without_Proper_Scope()
        {
            // Arrange
            var fakeNowProv = new FakeNowProv { }.Generate();
            var updatedNowProvDto = new FakeNowProvForUpdateDto { }.Generate();
            _client.AddAuth();

            await InsertAsync(fakeNowProv);

            // Act
            var route = ApiRoutes.NowProvs.Put.Replace(ApiRoutes.NowProvs.Id, fakeNowProv.Id.ToString());
            var result = await _client.PutJsonRequestAsync(route, updatedNowProvDto);

            // Assert
            result.StatusCode.Should().Be(403);
        }
    }
}