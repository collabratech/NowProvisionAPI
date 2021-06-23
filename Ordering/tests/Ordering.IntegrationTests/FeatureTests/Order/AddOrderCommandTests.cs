namespace Ordering.IntegrationTests.FeatureTests.Order
{
    using Ordering.SharedTestHelpers.Fakes.Order;
    using Ordering.IntegrationTests.TestUtilities;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using System.Threading.Tasks;
    using Ordering.WebApi.Features.Orders;
    using static TestFixture;
    using System;
    using Ordering.Core.Exceptions;

    public class AddOrderCommandTests : TestBase
    {
        [Test]
        public async Task AddOrderCommand_Adds_New_Order_To_Db()
        {
            // Arrange
            var fakeOrderOne = new FakeOrderForCreationDto { }.Generate();

            // Act
            var command = new AddOrder.AddOrderCommand(fakeOrderOne);
            var orderReturned = await SendAsync(command);
            var orderCreated = await ExecuteDbContextAsync(db => db.Orders.SingleOrDefaultAsync());

            // Assert
            orderReturned.Should().BeEquivalentTo(fakeOrderOne, options =>
                options.ExcludingMissingMembers());
            orderCreated.Should().BeEquivalentTo(fakeOrderOne, options =>
                options.ExcludingMissingMembers());
        }

        [Test]
        public async Task AddOrderCommand_Throws_Conflict_When_PK_Guid_Exists()
        {
            // Arrange
            var FakeOrder = new FakeOrder { }.Generate();
            var conflictRecord = new FakeOrderForCreationDto { }.Generate();
            conflictRecord.Id = FakeOrder.Id;

            await InsertAsync(FakeOrder);

            // Act
            var command = new AddOrder.AddOrderCommand(conflictRecord);
            Func<Task> act = () => SendAsync(command);

            // Assert
            act.Should().Throw<ConflictException>();
        }
    }
}