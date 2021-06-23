namespace Ordering.FunctionalTests.FunctionalTests.Order
{
    using Ordering.SharedTestHelpers.Fakes.Order;
    using Ordering.Core.Dtos.Order;
    using Ordering.FunctionalTests.TestUtilities;
    using Microsoft.AspNetCore.JsonPatch;
    using FluentAssertions;
    using NUnit.Framework;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class PartialOrderUpdateTests : TestBase
    {
        [Test]
        public async Task Patch_Order_Returns_NoContent_WithAuth()
        {
            // Arrange
            var fakeOrder = new FakeOrder { }.Generate();
            var patchDoc = new JsonPatchDocument<OrderForUpdateDto>();
            patchDoc.Replace(o => o.ProductHandle, "Easily Identified Value For Test");

            _client.AddAuth(new[] {"order.update"});

            await InsertAsync(fakeOrder);

            // Act
            var route = ApiRoutes.Orders.Patch.Replace(ApiRoutes.Orders.Id, fakeOrder.Id.ToString());
            var result = await _client.PatchJsonRequestAsync(route, patchDoc);

            // Assert
            result.StatusCode.Should().Be(204);
        }
            
        [Test]
        public async Task Patch_Order_Returns_Unauthorized_Without_Valid_Token()
        {
            // Arrange
            var fakeOrder = new FakeOrder { }.Generate();
            var patchDoc = new JsonPatchDocument<OrderForUpdateDto>();
            patchDoc.Replace(o => o.ProductHandle, "Easily Identified Value For Test");

            await InsertAsync(fakeOrder);

            // Act
            var route = ApiRoutes.Orders.Patch.Replace(ApiRoutes.Orders.Id, fakeOrder.Id.ToString());
            var result = await _client.PatchJsonRequestAsync(route, patchDoc);

            // Assert
            result.StatusCode.Should().Be(401);
        }
            
        [Test]
        public async Task Patch_Order_Returns_Forbidden_Without_Proper_Scope()
        {
            // Arrange
            var fakeOrder = new FakeOrder { }.Generate();
            var patchDoc = new JsonPatchDocument<OrderForUpdateDto>();
            patchDoc.Replace(o => o.ProductHandle, "Easily Identified Value For Test");
            _client.AddAuth();

            await InsertAsync(fakeOrder);

            // Act
            var route = ApiRoutes.Orders.Patch.Replace(ApiRoutes.Orders.Id, fakeOrder.Id.ToString());
            var result = await _client.PatchJsonRequestAsync(route, patchDoc);

            // Assert
            result.StatusCode.Should().Be(403);
        }
    }
}