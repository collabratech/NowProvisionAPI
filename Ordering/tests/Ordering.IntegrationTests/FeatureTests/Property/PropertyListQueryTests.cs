namespace Ordering.IntegrationTests.FeatureTests.Property
{
    using Ordering.Core.Dtos.Property;
    using Ordering.SharedTestHelpers.Fakes.Property;
    using Ordering.Core.Exceptions;
    using Ordering.WebApi.Features.Propertys;
    using FluentAssertions;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using static TestFixture;

    public class PropertyListQueryTests : TestBase
    {
        
        [Test]
        public async Task PropertyListQuery_Returns_Resource_With_Accurate_Props()
        {
            // Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            var queryParameters = new PropertyParametersDto();

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            // Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys.Should().HaveCount(2);
        }
        
        [Test]
        public async Task PropertyListQuery_Returns_Expected_Page_Size_And_Number()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            var fakePropertyThree = new FakeProperty { }.Generate();
            var queryParameters = new PropertyParametersDto() { PageSize = 1, PageNumber = 2 };

            await InsertAsync(fakePropertyOne, fakePropertyTwo, fakePropertyThree);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys.Should().HaveCount(1);
        }
        
        [Test]
        public async Task PropertyListQuery_Throws_ApiException_When_Null_Query_Parameters()
        {
            // Arrange
            // N/A

            // Act
            var query = new GetPropertyList.PropertyListQuery(null);
            Func<Task> act = () => SendAsync(query);

            // Assert
            act.Should().Throw<ApiException>();
        }
        
        [Test]
        public async Task PropertyListQuery_Returns_Sorted_Property_Slug_List_In_Asc_Order()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.Slug = "bravo";
            fakePropertyTwo.Slug = "alpha";
            var queryParameters = new PropertyParametersDto() { SortOrder = "Slug" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
            propertys
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Returns_Sorted_Property_Slug_List_In_Desc_Order()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.Slug = "bravo";
            fakePropertyTwo.Slug = "alpha";
            var queryParameters = new PropertyParametersDto() { SortOrder = "Slug" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
            propertys
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Returns_Sorted_Property_ContractType_List_In_Asc_Order()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.ContractType = "bravo";
            fakePropertyTwo.ContractType = "alpha";
            var queryParameters = new PropertyParametersDto() { SortOrder = "ContractType" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
            propertys
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Returns_Sorted_Property_ContractType_List_In_Desc_Order()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.ContractType = "bravo";
            fakePropertyTwo.ContractType = "alpha";
            var queryParameters = new PropertyParametersDto() { SortOrder = "ContractType" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
            propertys
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Returns_Sorted_Property_Country_List_In_Asc_Order()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.Country = "bravo";
            fakePropertyTwo.Country = "alpha";
            var queryParameters = new PropertyParametersDto() { SortOrder = "Country" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
            propertys
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Returns_Sorted_Property_Country_List_In_Desc_Order()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.Country = "bravo";
            fakePropertyTwo.Country = "alpha";
            var queryParameters = new PropertyParametersDto() { SortOrder = "Country" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
            propertys
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Returns_Sorted_Property_Address_List_In_Asc_Order()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.Address = "bravo";
            fakePropertyTwo.Address = "alpha";
            var queryParameters = new PropertyParametersDto() { SortOrder = "Address" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
            propertys
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Returns_Sorted_Property_Address_List_In_Desc_Order()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.Address = "bravo";
            fakePropertyTwo.Address = "alpha";
            var queryParameters = new PropertyParametersDto() { SortOrder = "Address" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
            propertys
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Returns_Sorted_Property_CityState_List_In_Asc_Order()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.CityState = "bravo";
            fakePropertyTwo.CityState = "alpha";
            var queryParameters = new PropertyParametersDto() { SortOrder = "CityState" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
            propertys
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Returns_Sorted_Property_CityState_List_In_Desc_Order()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.CityState = "bravo";
            fakePropertyTwo.CityState = "alpha";
            var queryParameters = new PropertyParametersDto() { SortOrder = "CityState" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
            propertys
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Returns_Sorted_Property_ZipCode_List_In_Asc_Order()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.ZipCode = "bravo";
            fakePropertyTwo.ZipCode = "alpha";
            var queryParameters = new PropertyParametersDto() { SortOrder = "ZipCode" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
            propertys
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Returns_Sorted_Property_ZipCode_List_In_Desc_Order()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.ZipCode = "bravo";
            fakePropertyTwo.ZipCode = "alpha";
            var queryParameters = new PropertyParametersDto() { SortOrder = "ZipCode" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
            propertys
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Returns_Sorted_Property_Price_List_In_Asc_Order()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.Price = "bravo";
            fakePropertyTwo.Price = "alpha";
            var queryParameters = new PropertyParametersDto() { SortOrder = "Price" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
            propertys
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Returns_Sorted_Property_Price_List_In_Desc_Order()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.Price = "bravo";
            fakePropertyTwo.Price = "alpha";
            var queryParameters = new PropertyParametersDto() { SortOrder = "Price" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
            propertys
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Returns_Sorted_Property_Bedrooms_List_In_Asc_Order()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.Bedrooms = "bravo";
            fakePropertyTwo.Bedrooms = "alpha";
            var queryParameters = new PropertyParametersDto() { SortOrder = "Bedrooms" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
            propertys
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Returns_Sorted_Property_Bedrooms_List_In_Desc_Order()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.Bedrooms = "bravo";
            fakePropertyTwo.Bedrooms = "alpha";
            var queryParameters = new PropertyParametersDto() { SortOrder = "Bedrooms" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
            propertys
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Returns_Sorted_Property_Bathrooms_List_In_Asc_Order()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.Bathrooms = "bravo";
            fakePropertyTwo.Bathrooms = "alpha";
            var queryParameters = new PropertyParametersDto() { SortOrder = "Bathrooms" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
            propertys
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Returns_Sorted_Property_Bathrooms_List_In_Desc_Order()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.Bathrooms = "bravo";
            fakePropertyTwo.Bathrooms = "alpha";
            var queryParameters = new PropertyParametersDto() { SortOrder = "Bathrooms" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
            propertys
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Returns_Sorted_Property_Area_List_In_Asc_Order()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.Area = "bravo";
            fakePropertyTwo.Area = "alpha";
            var queryParameters = new PropertyParametersDto() { SortOrder = "Area" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
            propertys
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Returns_Sorted_Property_Area_List_In_Desc_Order()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.Area = "bravo";
            fakePropertyTwo.Area = "alpha";
            var queryParameters = new PropertyParametersDto() { SortOrder = "Area" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
            propertys
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Returns_Sorted_Property_Headline_List_In_Asc_Order()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.Headline = "bravo";
            fakePropertyTwo.Headline = "alpha";
            var queryParameters = new PropertyParametersDto() { SortOrder = "Headline" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
            propertys
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Returns_Sorted_Property_Headline_List_In_Desc_Order()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.Headline = "bravo";
            fakePropertyTwo.Headline = "alpha";
            var queryParameters = new PropertyParametersDto() { SortOrder = "Headline" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
            propertys
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Returns_Sorted_Property_Description_List_In_Asc_Order()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.Description = "bravo";
            fakePropertyTwo.Description = "alpha";
            var queryParameters = new PropertyParametersDto() { SortOrder = "Description" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
            propertys
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Returns_Sorted_Property_Description_List_In_Desc_Order()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.Description = "bravo";
            fakePropertyTwo.Description = "alpha";
            var queryParameters = new PropertyParametersDto() { SortOrder = "Description" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
            propertys
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Returns_Sorted_Property_ParkingSpaces_List_In_Asc_Order()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.ParkingSpaces = "bravo";
            fakePropertyTwo.ParkingSpaces = "alpha";
            var queryParameters = new PropertyParametersDto() { SortOrder = "ParkingSpaces" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
            propertys
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Returns_Sorted_Property_ParkingSpaces_List_In_Desc_Order()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.ParkingSpaces = "bravo";
            fakePropertyTwo.ParkingSpaces = "alpha";
            var queryParameters = new PropertyParametersDto() { SortOrder = "ParkingSpaces" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
            propertys
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Returns_Sorted_Property_Type_List_In_Asc_Order()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.Type = "bravo";
            fakePropertyTwo.Type = "alpha";
            var queryParameters = new PropertyParametersDto() { SortOrder = "Type" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
            propertys
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Returns_Sorted_Property_Type_List_In_Desc_Order()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.Type = "bravo";
            fakePropertyTwo.Type = "alpha";
            var queryParameters = new PropertyParametersDto() { SortOrder = "Type" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
            propertys
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Returns_Sorted_Property_YearBuilt_List_In_Asc_Order()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.YearBuilt = "bravo";
            fakePropertyTwo.YearBuilt = "alpha";
            var queryParameters = new PropertyParametersDto() { SortOrder = "YearBuilt" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
            propertys
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Returns_Sorted_Property_YearBuilt_List_In_Desc_Order()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.YearBuilt = "bravo";
            fakePropertyTwo.YearBuilt = "alpha";
            var queryParameters = new PropertyParametersDto() { SortOrder = "YearBuilt" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
            propertys
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Returns_Sorted_Property_BuiltArea_List_In_Asc_Order()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.BuiltArea = "bravo";
            fakePropertyTwo.BuiltArea = "alpha";
            var queryParameters = new PropertyParametersDto() { SortOrder = "BuiltArea" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
            propertys
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Returns_Sorted_Property_BuiltArea_List_In_Desc_Order()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.BuiltArea = "bravo";
            fakePropertyTwo.BuiltArea = "alpha";
            var queryParameters = new PropertyParametersDto() { SortOrder = "BuiltArea" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
            propertys
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Returns_Sorted_Property_LotSize_List_In_Asc_Order()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.LotSize = "bravo";
            fakePropertyTwo.LotSize = "alpha";
            var queryParameters = new PropertyParametersDto() { SortOrder = "LotSize" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
            propertys
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Returns_Sorted_Property_LotSize_List_In_Desc_Order()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.LotSize = "bravo";
            fakePropertyTwo.LotSize = "alpha";
            var queryParameters = new PropertyParametersDto() { SortOrder = "LotSize" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
            propertys
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyOne, options =>
                    options.ExcludingMissingMembers());
        }

        
        [Test]
        public async Task PropertyListQuery_Filters_Property_PropertyId()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.PropertyId = Guid.NewGuid();
            fakePropertyTwo.PropertyId = Guid.NewGuid();
            var queryParameters = new PropertyParametersDto() { Filters = $"PropertyId == {fakePropertyTwo.PropertyId}" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys.Should().HaveCount(1);
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Filters_Property_Slug()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.Slug = "alpha";
            fakePropertyTwo.Slug = "bravo";
            var queryParameters = new PropertyParametersDto() { Filters = $"Slug == {fakePropertyTwo.Slug}" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys.Should().HaveCount(1);
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Filters_Property_ContractType()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.ContractType = "alpha";
            fakePropertyTwo.ContractType = "bravo";
            var queryParameters = new PropertyParametersDto() { Filters = $"ContractType == {fakePropertyTwo.ContractType}" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys.Should().HaveCount(1);
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Filters_Property_Country()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.Country = "alpha";
            fakePropertyTwo.Country = "bravo";
            var queryParameters = new PropertyParametersDto() { Filters = $"Country == {fakePropertyTwo.Country}" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys.Should().HaveCount(1);
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Filters_Property_HideAddress()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.HideAddress = false;
            fakePropertyTwo.HideAddress = true;
            var queryParameters = new PropertyParametersDto() { Filters = $"HideAddress == {fakePropertyTwo.HideAddress}" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys.Should().HaveCount(1);
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Filters_Property_Address()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.Address = "alpha";
            fakePropertyTwo.Address = "bravo";
            var queryParameters = new PropertyParametersDto() { Filters = $"Address == {fakePropertyTwo.Address}" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys.Should().HaveCount(1);
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Filters_Property_CityState()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.CityState = "alpha";
            fakePropertyTwo.CityState = "bravo";
            var queryParameters = new PropertyParametersDto() { Filters = $"CityState == {fakePropertyTwo.CityState}" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys.Should().HaveCount(1);
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Filters_Property_ZipCode()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.ZipCode = "alpha";
            fakePropertyTwo.ZipCode = "bravo";
            var queryParameters = new PropertyParametersDto() { Filters = $"ZipCode == {fakePropertyTwo.ZipCode}" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys.Should().HaveCount(1);
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Filters_Property_Price()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.Price = "alpha";
            fakePropertyTwo.Price = "bravo";
            var queryParameters = new PropertyParametersDto() { Filters = $"Price == {fakePropertyTwo.Price}" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys.Should().HaveCount(1);
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Filters_Property_Bedrooms()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.Bedrooms = "alpha";
            fakePropertyTwo.Bedrooms = "bravo";
            var queryParameters = new PropertyParametersDto() { Filters = $"Bedrooms == {fakePropertyTwo.Bedrooms}" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys.Should().HaveCount(1);
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Filters_Property_Bathrooms()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.Bathrooms = "alpha";
            fakePropertyTwo.Bathrooms = "bravo";
            var queryParameters = new PropertyParametersDto() { Filters = $"Bathrooms == {fakePropertyTwo.Bathrooms}" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys.Should().HaveCount(1);
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Filters_Property_Area()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.Area = "alpha";
            fakePropertyTwo.Area = "bravo";
            var queryParameters = new PropertyParametersDto() { Filters = $"Area == {fakePropertyTwo.Area}" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys.Should().HaveCount(1);
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Filters_Property_Headline()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.Headline = "alpha";
            fakePropertyTwo.Headline = "bravo";
            var queryParameters = new PropertyParametersDto() { Filters = $"Headline == {fakePropertyTwo.Headline}" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys.Should().HaveCount(1);
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Filters_Property_Description()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.Description = "alpha";
            fakePropertyTwo.Description = "bravo";
            var queryParameters = new PropertyParametersDto() { Filters = $"Description == {fakePropertyTwo.Description}" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys.Should().HaveCount(1);
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Filters_Property_ParkingSpaces()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.ParkingSpaces = "alpha";
            fakePropertyTwo.ParkingSpaces = "bravo";
            var queryParameters = new PropertyParametersDto() { Filters = $"ParkingSpaces == {fakePropertyTwo.ParkingSpaces}" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys.Should().HaveCount(1);
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Filters_Property_Type()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.Type = "alpha";
            fakePropertyTwo.Type = "bravo";
            var queryParameters = new PropertyParametersDto() { Filters = $"Type == {fakePropertyTwo.Type}" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys.Should().HaveCount(1);
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Filters_Property_YearBuilt()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.YearBuilt = "alpha";
            fakePropertyTwo.YearBuilt = "bravo";
            var queryParameters = new PropertyParametersDto() { Filters = $"YearBuilt == {fakePropertyTwo.YearBuilt}" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys.Should().HaveCount(1);
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Filters_Property_BuiltArea()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.BuiltArea = "alpha";
            fakePropertyTwo.BuiltArea = "bravo";
            var queryParameters = new PropertyParametersDto() { Filters = $"BuiltArea == {fakePropertyTwo.BuiltArea}" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys.Should().HaveCount(1);
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyListQuery_Filters_Property_LotSize()
        {
            //Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var fakePropertyTwo = new FakeProperty { }.Generate();
            fakePropertyOne.LotSize = "alpha";
            fakePropertyTwo.LotSize = "bravo";
            var queryParameters = new PropertyParametersDto() { Filters = $"LotSize == {fakePropertyTwo.LotSize}" };

            await InsertAsync(fakePropertyOne, fakePropertyTwo);

            //Act
            var query = new GetPropertyList.PropertyListQuery(queryParameters);
            var propertys = await SendAsync(query);

            // Assert
            propertys.Should().HaveCount(1);
            propertys
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakePropertyTwo, options =>
                    options.ExcludingMissingMembers());
        }

    }
}