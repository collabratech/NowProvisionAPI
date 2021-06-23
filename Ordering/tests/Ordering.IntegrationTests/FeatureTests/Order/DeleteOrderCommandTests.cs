namespace Ordering.IntegrationTests.FeatureTests.Order
{
    using Ordering.SharedTestHelpers.Fakes.Order;
    using Ordering.IntegrationTests.TestUtilities;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System;
    using System.Threading.Tasks;
    using Ordering.WebApi.Features.Orders;
    using static TestFixture;

    public class DeleteOrderCommandTests : TestBase
    {
        [Test]
        public async Task DeleteOrderCommand_Deletes_Order_From_Db()
        {
            // Arrange
            var fakeOrderOne = new FakeOrder { }.Generate();
            await InsertAsync(fakeOrderOne);
            var order = await ExecuteDbContextAsync(db => db.Orders.SingleOrDefaultAsync());
            var id = order.Id;

            // Act
            var command = new DeleteOrder.DeleteOrderCommand(id);
            await SendAsync(command);
            var orders = await ExecuteDbContextAsync(db => db.Orders.ToListAsync());

            // Assert
            orders.Count.Should().Be(0);
        }

        [Test]
        public async Task DeleteOrderCommand_Throws_KeyNotFoundException_When_Record_Does_Not_Exist()
        {
            // Arrange
            var badId = Guid.NewGuid();

            // Act
            var command = new DeleteOrder.DeleteOrderCommand(badId);
            Func<Task> act = () => SendAsync(command);

            // Assert
            act.Should().Throw<KeyNotFoundException>();
        }
    }
}