namespace Ordering.FunctionalTests.FunctionalTests.Property
{
    using Ordering.SharedTestHelpers.Fakes.Property;
    using Ordering.FunctionalTests.TestUtilities;
    using FluentAssertions;
    using NUnit.Framework;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class CreatePropertyTests : TestBase
    {
        [Test]
        public async Task Create_Property_Returns_Created()
        {
            // Arrange
            var fakeProperty = new FakePropertyForCreationDto { }.Generate();

            // Act
            var route = ApiRoutes.Propertys.Create;
            var result = await _client.PostJsonRequestAsync(route, fakeProperty);

            // Assert
            result.StatusCode.Should().Be(201);
        }
    }
}