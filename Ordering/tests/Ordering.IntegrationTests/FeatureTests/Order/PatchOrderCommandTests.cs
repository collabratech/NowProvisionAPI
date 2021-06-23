namespace Ordering.IntegrationTests.FeatureTests.Order
{
    using Ordering.SharedTestHelpers.Fakes.Order;
    using Ordering.IntegrationTests.TestUtilities;
    using Ordering.Core.Dtos.Order;
    using Ordering.Core.Exceptions;
    using Ordering.WebApi.Features.Orders;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.JsonPatch;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using static TestFixture;

    public class PatchOrderCommandTests : TestBase
    {
        [Test]
        public async Task PatchOrderCommand_Updates_Existing_Order_In_Db()
        {
            // Arrange
            var fakeOrderOne = new FakeOrder { }.Generate();
            await InsertAsync(fakeOrderOne);
            var order = await ExecuteDbContextAsync(db => db.Orders.SingleOrDefaultAsync());
            var id = order.Id;

            var patchDoc = new JsonPatchDocument<OrderForUpdateDto>();
            var newValue = "Easily Identified Value For Test";
            patchDoc.Replace(o => o.ProductHandle, newValue);

            // Act
            var command = new PatchOrder.PatchOrderCommand(id, patchDoc);
            await SendAsync(command);
            var updatedOrder = await ExecuteDbContextAsync(db => db.Orders.Where(o => o.Id == id).SingleOrDefaultAsync());

            // Assert
            updatedOrder.ProductHandle.Should().Be(newValue);
        }
        
        [Test]
        public async Task PatchOrderCommand_Throws_KeyNotFoundException_When_Bad_PK()
        {
            // Arrange
            var badId = Guid.NewGuid();
            var patchDoc = new JsonPatchDocument<OrderForUpdateDto>();

            // Act
            var command = new PatchOrder.PatchOrderCommand(badId, patchDoc);
            Func<Task> act = () => SendAsync(command);

            // Assert
            act.Should().Throw<KeyNotFoundException>();
        }

        [Test]
        public async Task PatchOrderCommand_Throws_ApiException_When_Null_Patchdoc()
        {
            // Arrange
            var randomId = Guid.NewGuid();

            // Act
            var command = new PatchOrder.PatchOrderCommand(randomId, null);
            Func<Task> act = () => SendAsync(command);

            // Assert
            act.Should().Throw<ApiException>();
        }
    }
}