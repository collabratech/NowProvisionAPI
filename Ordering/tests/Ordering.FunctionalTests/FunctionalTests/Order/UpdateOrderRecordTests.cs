namespace Ordering.FunctionalTests.FunctionalTests.Order
{
    using Ordering.SharedTestHelpers.Fakes.Order;
    using Ordering.FunctionalTests.TestUtilities;
    using FluentAssertions;
    using NUnit.Framework;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class UpdateOrderRecordTests : TestBase
    {
        [Test]
        public async Task Put_Order_Returns_NoContent_WithAuth()
        {
            // Arrange
            var fakeOrder = new FakeOrder { }.Generate();
            var updatedOrderDto = new FakeOrderForUpdateDto { }.Generate();

            _client.AddAuth(new[] {"order.update"});

            await InsertAsync(fakeOrder);

            // Act
            var route = ApiRoutes.Orders.Put.Replace(ApiRoutes.Orders.Id, fakeOrder.Id.ToString());
            var result = await _client.PutJsonRequestAsync(route, updatedOrderDto);

            // Assert
            result.StatusCode.Should().Be(204);
        }
            
        [Test]
        public async Task Put_Order_Returns_Unauthorized_Without_Valid_Token()
        {
            // Arrange
            var fakeOrder = new FakeOrder { }.Generate();
            var updatedOrderDto = new FakeOrderForUpdateDto { }.Generate();

            await InsertAsync(fakeOrder);

            // Act
            var route = ApiRoutes.Orders.Put.Replace(ApiRoutes.Orders.Id, fakeOrder.Id.ToString());
            var result = await _client.PutJsonRequestAsync(route, updatedOrderDto);

            // Assert
            result.StatusCode.Should().Be(401);
        }
            
        [Test]
        public async Task Put_Order_Returns_Forbidden_Without_Proper_Scope()
        {
            // Arrange
            var fakeOrder = new FakeOrder { }.Generate();
            var updatedOrderDto = new FakeOrderForUpdateDto { }.Generate();
            _client.AddAuth();

            await InsertAsync(fakeOrder);

            // Act
            var route = ApiRoutes.Orders.Put.Replace(ApiRoutes.Orders.Id, fakeOrder.Id.ToString());
            var result = await _client.PutJsonRequestAsync(route, updatedOrderDto);

            // Assert
            result.StatusCode.Should().Be(403);
        }
    }
}