namespace NowProvisionAPI.FunctionalTests.FunctionalTests.NowProv
{
    using NowProvisionAPI.SharedTestHelpers.Fakes.NowProv;
    using NowProvisionAPI.FunctionalTests.TestUtilities;
    using FluentAssertions;
    using NUnit.Framework;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class GetNowProvTests : TestBase
    {
        [Test]
        public async Task Get_NowProv_Record_Returns_NoContent_WithAuth()
        {
            // Arrange
            var fakeNowProv = new FakeNowProv { }.Generate();

            _client.AddAuth(new[] {"NowProv.read"});

            await InsertAsync(fakeNowProv);

            // Act
            var route = ApiRoutes.NowProvs.GetRecord.Replace(ApiRoutes.NowProvs.Id, fakeNowProv.Id.ToString());
            var result = await _client.GetRequestAsync(route);

            // Assert
            result.StatusCode.Should().Be(200);
        }
            
        [Test]
        public async Task Get_NowProv_Record_Returns_Unauthorized_Without_Valid_Token()
        {
            // Arrange
            var fakeNowProv = new FakeNowProv { }.Generate();

            await InsertAsync(fakeNowProv);

            // Act
            var route = ApiRoutes.NowProvs.GetRecord.Replace(ApiRoutes.NowProvs.Id, fakeNowProv.Id.ToString());
            var result = await _client.GetRequestAsync(route);

            // Assert
            result.StatusCode.Should().Be(401);
        }
            
        [Test]
        public async Task Get_NowProv_Record_Returns_Forbidden_Without_Proper_Scope()
        {
            // Arrange
            var fakeNowProv = new FakeNowProv { }.Generate();
            _client.AddAuth();

            await InsertAsync(fakeNowProv);

            // Act
            var route = ApiRoutes.NowProvs.GetRecord.Replace(ApiRoutes.NowProvs.Id, fakeNowProv.Id.ToString());
            var result = await _client.GetRequestAsync(route);

            // Assert
            result.StatusCode.Should().Be(403);
        }
    }
}