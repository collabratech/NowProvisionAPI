namespace Ordering.FunctionalTests.FunctionalTests.Office
{
    using Ordering.SharedTestHelpers.Fakes.Office;
    using Ordering.FunctionalTests.TestUtilities;
    using FluentAssertions;
    using NUnit.Framework;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class CreateOfficeTests : TestBase
    {
        [Test]
        public async Task Create_Office_Returns_Created()
        {
            // Arrange
            var fakeOffice = new FakeOfficeForCreationDto { }.Generate();

            // Act
            var route = ApiRoutes.Offices.Create;
            var result = await _client.PostJsonRequestAsync(route, fakeOffice);

            // Assert
            result.StatusCode.Should().Be(201);
        }
    }
}