namespace NowProvisionAPI.FunctionalTests.FunctionalTests.NowProv
{
    using NowProvisionAPI.SharedTestHelpers.Fakes.NowProv;
    using NowProvisionAPI.FunctionalTests.TestUtilities;
    using FluentAssertions;
    using NUnit.Framework;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class CreateNowProvTests : TestBase
    {
        [Test]
        public async Task Create_NowProv_Returns_Created_WithAuth()
        {
            // Arrange
            var fakeNowProv = new FakeNowProvForCreationDto { }.Generate();

            _client.AddAuth(new[] {"NowProv.add"});

            // Act
            var route = ApiRoutes.NowProvs.Create;
            var result = await _client.PostJsonRequestAsync(route, fakeNowProv);

            // Assert
            result.StatusCode.Should().Be(201);
        }
            
        [Test]
        public async Task Create_NowProv_Returns_Unauthorized_Without_Valid_Token()
        {
            // Arrange
            var fakeNowProv = new FakeNowProv { }.Generate();

            await InsertAsync(fakeNowProv);

            // Act
            var route = ApiRoutes.NowProvs.Create;
            var result = await _client.PostJsonRequestAsync(route, fakeNowProv);

            // Assert
            result.StatusCode.Should().Be(401);
        }
            
        [Test]
        public async Task Create_NowProv_Returns_Forbidden_Without_Proper_Scope()
        {
            // Arrange
            var fakeNowProv = new FakeNowProv { }.Generate();
            _client.AddAuth();

            await InsertAsync(fakeNowProv);

            // Act
            var route = ApiRoutes.NowProvs.Create;
            var result = await _client.PostJsonRequestAsync(route, fakeNowProv);

            // Assert
            result.StatusCode.Should().Be(403);
        }
    }
}