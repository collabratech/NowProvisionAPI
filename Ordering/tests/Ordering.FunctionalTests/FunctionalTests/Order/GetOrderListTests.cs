namespace Ordering.FunctionalTests.FunctionalTests.Order
{
    using Ordering.SharedTestHelpers.Fakes.Order;
    using Ordering.FunctionalTests.TestUtilities;
    using FluentAssertions;
    using NUnit.Framework;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class GetOrderListTests : TestBase
    {
        [Test]
        public async Task Get_Order_List_Returns_NoContent_WithAuth()
        {
            // Arrange
            _client.AddAuth(new[] {"order.read"});

            // Act
            var result = await _client.GetRequestAsync(ApiRoutes.Orders.GetList);

            // Assert
            result.StatusCode.Should().Be(200);
        }
            
        [Test]
        public async Task Get_Order_List_Returns_Unauthorized_Without_Valid_Token()
        {
            // Arrange
            // N/A

            // Act
            var result = await _client.GetRequestAsync(ApiRoutes.Orders.GetList);

            // Assert
            result.StatusCode.Should().Be(401);
        }
            
        [Test]
        public async Task Get_Order_List_Returns_Forbidden_Without_Proper_Scope()
        {
            // Arrange
            _client.AddAuth();

            // Act
            var result = await _client.GetRequestAsync(ApiRoutes.Orders.GetList);

            // Assert
            result.StatusCode.Should().Be(403);
        }
    }
}