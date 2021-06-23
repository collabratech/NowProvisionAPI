namespace NowProvisionAPI.IntegrationTests.FeatureTests.Office
{
    using NowProvisionAPI.Core.Dtos.Office;
    using NowProvisionAPI.SharedTestHelpers.Fakes.Office;
    using NowProvisionAPI.Core.Exceptions;
    using NowProvisionAPI.WebApi.Features.Offices;
    using FluentAssertions;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using static TestFixture;

    public class OfficeListQueryTests : TestBase
    {
        
        [Test]
        public async Task OfficeListQuery_Returns_Resource_With_Accurate_Props()
        {
            // Arrange
            var fakeOfficeOne = new FakeOffice { }.Generate();
            var fakeOfficeTwo = new FakeOffice { }.Generate();
            var queryParameters = new OfficeParametersDto();

            await InsertAsync(fakeOfficeOne, fakeOfficeTwo);

            // Act
            var query = new GetOfficeList.OfficeListQuery(queryParameters);
            var offices = await SendAsync(query);

            // Assert
            offices.Should().HaveCount(2);
        }
        
        [Test]
        public async Task OfficeListQuery_Returns_Expected_Page_Size_And_Number()
        {
            //Arrange
            var fakeOfficeOne = new FakeOffice { }.Generate();
            var fakeOfficeTwo = new FakeOffice { }.Generate();
            var fakeOfficeThree = new FakeOffice { }.Generate();
            var queryParameters = new OfficeParametersDto() { PageSize = 1, PageNumber = 2 };

            await InsertAsync(fakeOfficeOne, fakeOfficeTwo, fakeOfficeThree);

            //Act
            var query = new GetOfficeList.OfficeListQuery(queryParameters);
            var offices = await SendAsync(query);

            // Assert
            offices.Should().HaveCount(1);
        }
        
        [Test]
        public async Task OfficeListQuery_Throws_ApiException_When_Null_Query_Parameters()
        {
            // Arrange
            // N/A

            // Act
            var query = new GetOfficeList.OfficeListQuery(null);
            Func<Task> act = () => SendAsync(query);

            // Assert
            act.Should().Throw<ApiException>();
        }
        
        [Test]
        public async Task OfficeListQuery_Returns_Sorted_Office_Name_List_In_Asc_Order()
        {
            //Arrange
            var fakeOfficeOne = new FakeOffice { }.Generate();
            var fakeOfficeTwo = new FakeOffice { }.Generate();
            fakeOfficeOne.Name = "bravo";
            fakeOfficeTwo.Name = "alpha";
            var queryParameters = new OfficeParametersDto() { SortOrder = "Name" };

            await InsertAsync(fakeOfficeOne, fakeOfficeTwo);

            //Act
            var query = new GetOfficeList.OfficeListQuery(queryParameters);
            var offices = await SendAsync(query);

            // Assert
            offices
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeOfficeTwo, options =>
                    options.ExcludingMissingMembers());
            offices
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeOfficeOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task OfficeListQuery_Returns_Sorted_Office_Name_List_In_Desc_Order()
        {
            //Arrange
            var fakeOfficeOne = new FakeOffice { }.Generate();
            var fakeOfficeTwo = new FakeOffice { }.Generate();
            fakeOfficeOne.Name = "bravo";
            fakeOfficeTwo.Name = "alpha";
            var queryParameters = new OfficeParametersDto() { SortOrder = "Name" };

            await InsertAsync(fakeOfficeOne, fakeOfficeTwo);

            //Act
            var query = new GetOfficeList.OfficeListQuery(queryParameters);
            var offices = await SendAsync(query);

            // Assert
            offices
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeOfficeTwo, options =>
                    options.ExcludingMissingMembers());
            offices
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeOfficeOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task OfficeListQuery_Returns_Sorted_Office_Address_List_In_Asc_Order()
        {
            //Arrange
            var fakeOfficeOne = new FakeOffice { }.Generate();
            var fakeOfficeTwo = new FakeOffice { }.Generate();
            fakeOfficeOne.Address = "bravo";
            fakeOfficeTwo.Address = "alpha";
            var queryParameters = new OfficeParametersDto() { SortOrder = "Address" };

            await InsertAsync(fakeOfficeOne, fakeOfficeTwo);

            //Act
            var query = new GetOfficeList.OfficeListQuery(queryParameters);
            var offices = await SendAsync(query);

            // Assert
            offices
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeOfficeTwo, options =>
                    options.ExcludingMissingMembers());
            offices
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeOfficeOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task OfficeListQuery_Returns_Sorted_Office_Address_List_In_Desc_Order()
        {
            //Arrange
            var fakeOfficeOne = new FakeOffice { }.Generate();
            var fakeOfficeTwo = new FakeOffice { }.Generate();
            fakeOfficeOne.Address = "bravo";
            fakeOfficeTwo.Address = "alpha";
            var queryParameters = new OfficeParametersDto() { SortOrder = "Address" };

            await InsertAsync(fakeOfficeOne, fakeOfficeTwo);

            //Act
            var query = new GetOfficeList.OfficeListQuery(queryParameters);
            var offices = await SendAsync(query);

            // Assert
            offices
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeOfficeTwo, options =>
                    options.ExcludingMissingMembers());
            offices
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeOfficeOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task OfficeListQuery_Returns_Sorted_Office_CityState_List_In_Asc_Order()
        {
            //Arrange
            var fakeOfficeOne = new FakeOffice { }.Generate();
            var fakeOfficeTwo = new FakeOffice { }.Generate();
            fakeOfficeOne.CityState = "bravo";
            fakeOfficeTwo.CityState = "alpha";
            var queryParameters = new OfficeParametersDto() { SortOrder = "CityState" };

            await InsertAsync(fakeOfficeOne, fakeOfficeTwo);

            //Act
            var query = new GetOfficeList.OfficeListQuery(queryParameters);
            var offices = await SendAsync(query);

            // Assert
            offices
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeOfficeTwo, options =>
                    options.ExcludingMissingMembers());
            offices
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeOfficeOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task OfficeListQuery_Returns_Sorted_Office_CityState_List_In_Desc_Order()
        {
            //Arrange
            var fakeOfficeOne = new FakeOffice { }.Generate();
            var fakeOfficeTwo = new FakeOffice { }.Generate();
            fakeOfficeOne.CityState = "bravo";
            fakeOfficeTwo.CityState = "alpha";
            var queryParameters = new OfficeParametersDto() { SortOrder = "CityState" };

            await InsertAsync(fakeOfficeOne, fakeOfficeTwo);

            //Act
            var query = new GetOfficeList.OfficeListQuery(queryParameters);
            var offices = await SendAsync(query);

            // Assert
            offices
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeOfficeTwo, options =>
                    options.ExcludingMissingMembers());
            offices
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeOfficeOne, options =>
                    options.ExcludingMissingMembers());
        }

        
        [Test]
        public async Task OfficeListQuery_Filters_Office_OfficeId()
        {
            //Arrange
            var fakeOfficeOne = new FakeOffice { }.Generate();
            var fakeOfficeTwo = new FakeOffice { }.Generate();
            fakeOfficeOne.OfficeId = Guid.NewGuid();
            fakeOfficeTwo.OfficeId = Guid.NewGuid();
            var queryParameters = new OfficeParametersDto() { Filters = $"OfficeId == {fakeOfficeTwo.OfficeId}" };

            await InsertAsync(fakeOfficeOne, fakeOfficeTwo);

            //Act
            var query = new GetOfficeList.OfficeListQuery(queryParameters);
            var offices = await SendAsync(query);

            // Assert
            offices.Should().HaveCount(1);
            offices
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeOfficeTwo, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task OfficeListQuery_Filters_Office_Name()
        {
            //Arrange
            var fakeOfficeOne = new FakeOffice { }.Generate();
            var fakeOfficeTwo = new FakeOffice { }.Generate();
            fakeOfficeOne.Name = "alpha";
            fakeOfficeTwo.Name = "bravo";
            var queryParameters = new OfficeParametersDto() { Filters = $"Name == {fakeOfficeTwo.Name}" };

            await InsertAsync(fakeOfficeOne, fakeOfficeTwo);

            //Act
            var query = new GetOfficeList.OfficeListQuery(queryParameters);
            var offices = await SendAsync(query);

            // Assert
            offices.Should().HaveCount(1);
            offices
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeOfficeTwo, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task OfficeListQuery_Filters_Office_Address()
        {
            //Arrange
            var fakeOfficeOne = new FakeOffice { }.Generate();
            var fakeOfficeTwo = new FakeOffice { }.Generate();
            fakeOfficeOne.Address = "alpha";
            fakeOfficeTwo.Address = "bravo";
            var queryParameters = new OfficeParametersDto() { Filters = $"Address == {fakeOfficeTwo.Address}" };

            await InsertAsync(fakeOfficeOne, fakeOfficeTwo);

            //Act
            var query = new GetOfficeList.OfficeListQuery(queryParameters);
            var offices = await SendAsync(query);

            // Assert
            offices.Should().HaveCount(1);
            offices
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeOfficeTwo, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task OfficeListQuery_Filters_Office_CityState()
        {
            //Arrange
            var fakeOfficeOne = new FakeOffice { }.Generate();
            var fakeOfficeTwo = new FakeOffice { }.Generate();
            fakeOfficeOne.CityState = "alpha";
            fakeOfficeTwo.CityState = "bravo";
            var queryParameters = new OfficeParametersDto() { Filters = $"CityState == {fakeOfficeTwo.CityState}" };

            await InsertAsync(fakeOfficeOne, fakeOfficeTwo);

            //Act
            var query = new GetOfficeList.OfficeListQuery(queryParameters);
            var offices = await SendAsync(query);

            // Assert
            offices.Should().HaveCount(1);
            offices
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeOfficeTwo, options =>
                    options.ExcludingMissingMembers());
        }

    }
}