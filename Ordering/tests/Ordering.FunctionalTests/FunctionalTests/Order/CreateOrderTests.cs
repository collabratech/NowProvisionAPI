namespace Ordering.FunctionalTests.FunctionalTests.Order
{
    using Ordering.SharedTestHelpers.Fakes.Order;
    using Ordering.FunctionalTests.TestUtilities;
    using FluentAssertions;
    using NUnit.Framework;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class CreateOrderTests : TestBase
    {
        [Test]
        public async Task Create_Order_Returns_Created_WithAuth()
        {
            // Arrange
            var fakeOrder = new FakeOrderForCreationDto { }.Generate();

            _client.AddAuth(new[] {"order.add"});

            // Act
            var route = ApiRoutes.Orders.Create;
            var result = await _client.PostJsonRequestAsync(route, fakeOrder);

            // Assert
            result.StatusCode.Should().Be(201);
        }
            
        [Test]
        public async Task Create_Order_Returns_Unauthorized_Without_Valid_Token()
        {
            // Arrange
            var fakeOrder = new FakeOrder { }.Generate();

            await InsertAsync(fakeOrder);

            // Act
            var route = ApiRoutes.Orders.Create;
            var result = await _client.PostJsonRequestAsync(route, fakeOrder);

            // Assert
            result.StatusCode.Should().Be(401);
        }
            
        [Test]
        public async Task Create_Order_Returns_Forbidden_Without_Proper_Scope()
        {
            // Arrange
            var fakeOrder = new FakeOrder { }.Generate();
            _client.AddAuth();

            await InsertAsync(fakeOrder);

            // Act
            var route = ApiRoutes.Orders.Create;
            var result = await _client.PostJsonRequestAsync(route, fakeOrder);

            // Assert
            result.StatusCode.Should().Be(403);
        }
    }
}