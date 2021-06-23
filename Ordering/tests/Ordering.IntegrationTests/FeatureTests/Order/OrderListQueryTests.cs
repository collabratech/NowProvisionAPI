namespace Ordering.IntegrationTests.FeatureTests.Order
{
    using Ordering.Core.Dtos.Order;
    using Ordering.SharedTestHelpers.Fakes.Order;
    using Ordering.Core.Exceptions;
    using Ordering.WebApi.Features.Orders;
    using FluentAssertions;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using static TestFixture;

    public class OrderListQueryTests : TestBase
    {
        
        [Test]
        public async Task OrderListQuery_Returns_Resource_With_Accurate_Props()
        {
            // Arrange
            var fakeOrderOne = new FakeOrder { }.Generate();
            var fakeOrderTwo = new FakeOrder { }.Generate();
            var queryParameters = new OrderParametersDto();

            await InsertAsync(fakeOrderOne, fakeOrderTwo);

            // Act
            var query = new GetOrderList.OrderListQuery(queryParameters);
            var orders = await SendAsync(query);

            // Assert
            orders.Should().HaveCount(2);
        }
        
        [Test]
        public async Task OrderListQuery_Returns_Expected_Page_Size_And_Number()
        {
            //Arrange
            var fakeOrderOne = new FakeOrder { }.Generate();
            var fakeOrderTwo = new FakeOrder { }.Generate();
            var fakeOrderThree = new FakeOrder { }.Generate();
            var queryParameters = new OrderParametersDto() { PageSize = 1, PageNumber = 2 };

            await InsertAsync(fakeOrderOne, fakeOrderTwo, fakeOrderThree);

            //Act
            var query = new GetOrderList.OrderListQuery(queryParameters);
            var orders = await SendAsync(query);

            // Assert
            orders.Should().HaveCount(1);
        }
        
        [Test]
        public async Task OrderListQuery_Throws_ApiException_When_Null_Query_Parameters()
        {
            // Arrange
            // N/A

            // Act
            var query = new GetOrderList.OrderListQuery(null);
            Func<Task> act = () => SendAsync(query);

            // Assert
            act.Should().Throw<ApiException>();
        }
        
        [Test]
        public async Task OrderListQuery_Returns_Sorted_Order_ProductHandle_List_In_Asc_Order()
        {
            //Arrange
            var fakeOrderOne = new FakeOrder { }.Generate();
            var fakeOrderTwo = new FakeOrder { }.Generate();
            fakeOrderOne.ProductHandle = "bravo";
            fakeOrderTwo.ProductHandle = "alpha";
            var queryParameters = new OrderParametersDto() { SortOrder = "ProductHandle" };

            await InsertAsync(fakeOrderOne, fakeOrderTwo);

            //Act
            var query = new GetOrderList.OrderListQuery(queryParameters);
            var orders = await SendAsync(query);

            // Assert
            orders
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeOrderTwo, options =>
                    options.ExcludingMissingMembers());
            orders
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeOrderOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task OrderListQuery_Returns_Sorted_Order_ProductHandle_List_In_Desc_Order()
        {
            //Arrange
            var fakeOrderOne = new FakeOrder { }.Generate();
            var fakeOrderTwo = new FakeOrder { }.Generate();
            fakeOrderOne.ProductHandle = "bravo";
            fakeOrderTwo.ProductHandle = "alpha";
            var queryParameters = new OrderParametersDto() { SortOrder = "ProductHandle" };

            await InsertAsync(fakeOrderOne, fakeOrderTwo);

            //Act
            var query = new GetOrderList.OrderListQuery(queryParameters);
            var orders = await SendAsync(query);

            // Assert
            orders
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeOrderTwo, options =>
                    options.ExcludingMissingMembers());
            orders
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeOrderOne, options =>
                    options.ExcludingMissingMembers());
        }

        
        [Test]
        public async Task OrderListQuery_Filters_Order_Id()
        {
            //Arrange
            var fakeOrderOne = new FakeOrder { }.Generate();
            var fakeOrderTwo = new FakeOrder { }.Generate();
            fakeOrderOne.Id = Guid.NewGuid();
            fakeOrderTwo.Id = Guid.NewGuid();
            var queryParameters = new OrderParametersDto() { Filters = $"Id == {fakeOrderTwo.Id}" };

            await InsertAsync(fakeOrderOne, fakeOrderTwo);

            //Act
            var query = new GetOrderList.OrderListQuery(queryParameters);
            var orders = await SendAsync(query);

            // Assert
            orders.Should().HaveCount(1);
            orders
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeOrderTwo, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task OrderListQuery_Filters_Order_ProductHandle()
        {
            //Arrange
            var fakeOrderOne = new FakeOrder { }.Generate();
            var fakeOrderTwo = new FakeOrder { }.Generate();
            fakeOrderOne.ProductHandle = "alpha";
            fakeOrderTwo.ProductHandle = "bravo";
            var queryParameters = new OrderParametersDto() { Filters = $"ProductHandle == {fakeOrderTwo.ProductHandle}" };

            await InsertAsync(fakeOrderOne, fakeOrderTwo);

            //Act
            var query = new GetOrderList.OrderListQuery(queryParameters);
            var orders = await SendAsync(query);

            // Assert
            orders.Should().HaveCount(1);
            orders
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeOrderTwo, options =>
                    options.ExcludingMissingMembers());
        }

    }
}