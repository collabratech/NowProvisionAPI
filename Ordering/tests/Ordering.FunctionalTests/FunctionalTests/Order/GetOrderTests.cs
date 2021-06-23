namespace Ordering.FunctionalTests.FunctionalTests.Order
{
    using Ordering.SharedTestHelpers.Fakes.Order;
    using Ordering.FunctionalTests.TestUtilities;
    using FluentAssertions;
    using NUnit.Framework;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class GetOrderTests : TestBase
    {
        [Test]
        public async Task Get_Order_Record_Returns_NoContent_WithAuth()
        {
            // Arrange
            var fakeOrder = new FakeOrder { }.Generate();

            _client.AddAuth(new[] {"order.read"});

            await InsertAsync(fakeOrder);

            // Act
            var route = ApiRoutes.Orders.GetRecord.Replace(ApiRoutes.Orders.Id, fakeOrder.Id.ToString());
            var result = await _client.GetRequestAsync(route);

            // Assert
            result.StatusCode.Should().Be(200);
        }
            
        [Test]
        public async Task Get_Order_Record_Returns_Unauthorized_Without_Valid_Token()
        {
            // Arrange
            var fakeOrder = new FakeOrder { }.Generate();

            await InsertAsync(fakeOrder);

            // Act
            var route = ApiRoutes.Orders.GetRecord.Replace(ApiRoutes.Orders.Id, fakeOrder.Id.ToString());
            var result = await _client.GetRequestAsync(route);

            // Assert
            result.StatusCode.Should().Be(401);
        }
            
        [Test]
        public async Task Get_Order_Record_Returns_Forbidden_Without_Proper_Scope()
        {
            // Arrange
            var fakeOrder = new FakeOrder { }.Generate();
            _client.AddAuth();

            await InsertAsync(fakeOrder);

            // Act
            var route = ApiRoutes.Orders.GetRecord.Replace(ApiRoutes.Orders.Id, fakeOrder.Id.ToString());
            var result = await _client.GetRequestAsync(route);

            // Assert
            result.StatusCode.Should().Be(403);
        }
    }
}