namespace Ordering.IntegrationTests.FeatureTests.Order
{
    using Ordering.SharedTestHelpers.Fakes.Order;
    using Ordering.IntegrationTests.TestUtilities;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Ordering.WebApi.Features.Orders;
    using static TestFixture;

    public class OrderQueryTests : TestBase
    {
        [Test]
        public async Task OrderQuery_Returns_Resource_With_Accurate_Props()
        {
            // Arrange
            var fakeOrderOne = new FakeOrder { }.Generate();
            await InsertAsync(fakeOrderOne);

            // Act
            var query = new GetOrder.OrderQuery(fakeOrderOne.Id);
            var orders = await SendAsync(query);

            // Assert
            orders.Should().BeEquivalentTo(fakeOrderOne, options =>
                options.ExcludingMissingMembers());
        }

        [Test]
        public async Task OrderQuery_Throws_KeyNotFoundException_When_Record_Does_Not_Exist()
        {
            // Arrange
            var badId = Guid.NewGuid();

            // Act
            var query = new GetOrder.OrderQuery(badId);
            Func<Task> act = () => SendAsync(query);

            // Assert
            act.Should().Throw<KeyNotFoundException>();
        }
    }
}