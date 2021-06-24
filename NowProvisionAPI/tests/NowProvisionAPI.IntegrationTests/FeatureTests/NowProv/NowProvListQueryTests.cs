namespace NowProvisionAPI.IntegrationTests.FeatureTests.NowProv
{
    using NowProvisionAPI.Core.Dtos.NowProv;
    using NowProvisionAPI.SharedTestHelpers.Fakes.NowProv;
    using NowProvisionAPI.Core.Exceptions;
    using NowProvisionAPI.WebApi.Features.NowProvs;
    using FluentAssertions;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using static TestFixture;

    public class NowProvListQueryTests : TestBase
    {
        
        [Test]
        public async Task NowProvListQuery_Returns_Resource_With_Accurate_Props()
        {
            // Arrange
            var fakeNowProvOne = new FakeNowProv { }.Generate();
            var fakeNowProvTwo = new FakeNowProv { }.Generate();
            var queryParameters = new NowProvParametersDto();

            await InsertAsync(fakeNowProvOne, fakeNowProvTwo);

            // Act
            var query = new GetNowProvList.NowProvListQuery(queryParameters);
            var nowProvs = await SendAsync(query);

            // Assert
            nowProvs.Should().HaveCount(2);
        }
        
        [Test]
        public async Task NowProvListQuery_Returns_Expected_Page_Size_And_Number()
        {
            //Arrange
            var fakeNowProvOne = new FakeNowProv { }.Generate();
            var fakeNowProvTwo = new FakeNowProv { }.Generate();
            var fakeNowProvThree = new FakeNowProv { }.Generate();
            var queryParameters = new NowProvParametersDto() { PageSize = 1, PageNumber = 2 };

            await InsertAsync(fakeNowProvOne, fakeNowProvTwo, fakeNowProvThree);

            //Act
            var query = new GetNowProvList.NowProvListQuery(queryParameters);
            var nowProvs = await SendAsync(query);

            // Assert
            nowProvs.Should().HaveCount(1);
        }
        
        [Test]
        public async Task NowProvListQuery_Throws_ApiException_When_Null_Query_Parameters()
        {
            // Arrange
            // N/A

            // Act
            var query = new GetNowProvList.NowProvListQuery(null);
            Func<Task> act = () => SendAsync(query);

            // Assert
            act.Should().Throw<ApiException>();
        }
        
        [Test]
        public async Task NowProvListQuery_Returns_Sorted_NowProv_Events_List_In_Asc_Order()
        {
            //Arrange
            var fakeNowProvOne = new FakeNowProv { }.Generate();
            var fakeNowProvTwo = new FakeNowProv { }.Generate();
            fakeNowProvOne.Events = "bravo";
            fakeNowProvTwo.Events = "alpha";
            var queryParameters = new NowProvParametersDto() { SortOrder = "Events" };

            await InsertAsync(fakeNowProvOne, fakeNowProvTwo);

            //Act
            var query = new GetNowProvList.NowProvListQuery(queryParameters);
            var nowProvs = await SendAsync(query);

            // Assert
            nowProvs
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeNowProvTwo, options =>
                    options.ExcludingMissingMembers());
            nowProvs
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeNowProvOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task NowProvListQuery_Returns_Sorted_NowProv_Events_List_In_Desc_Order()
        {
            //Arrange
            var fakeNowProvOne = new FakeNowProv { }.Generate();
            var fakeNowProvTwo = new FakeNowProv { }.Generate();
            fakeNowProvOne.Events = "bravo";
            fakeNowProvTwo.Events = "alpha";
            var queryParameters = new NowProvParametersDto() { SortOrder = "Events" };

            await InsertAsync(fakeNowProvOne, fakeNowProvTwo);

            //Act
            var query = new GetNowProvList.NowProvListQuery(queryParameters);
            var nowProvs = await SendAsync(query);

            // Assert
            nowProvs
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeNowProvTwo, options =>
                    options.ExcludingMissingMembers());
            nowProvs
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeNowProvOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task NowProvListQuery_Returns_Sorted_NowProv_JobName_List_In_Asc_Order()
        {
            //Arrange
            var fakeNowProvOne = new FakeNowProv { }.Generate();
            var fakeNowProvTwo = new FakeNowProv { }.Generate();
            fakeNowProvOne.JobName = "bravo";
            fakeNowProvTwo.JobName = "alpha";
            var queryParameters = new NowProvParametersDto() { SortOrder = "JobName" };

            await InsertAsync(fakeNowProvOne, fakeNowProvTwo);

            //Act
            var query = new GetNowProvList.NowProvListQuery(queryParameters);
            var nowProvs = await SendAsync(query);

            // Assert
            nowProvs
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeNowProvTwo, options =>
                    options.ExcludingMissingMembers());
            nowProvs
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeNowProvOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task NowProvListQuery_Returns_Sorted_NowProv_JobName_List_In_Desc_Order()
        {
            //Arrange
            var fakeNowProvOne = new FakeNowProv { }.Generate();
            var fakeNowProvTwo = new FakeNowProv { }.Generate();
            fakeNowProvOne.JobName = "bravo";
            fakeNowProvTwo.JobName = "alpha";
            var queryParameters = new NowProvParametersDto() { SortOrder = "JobName" };

            await InsertAsync(fakeNowProvOne, fakeNowProvTwo);

            //Act
            var query = new GetNowProvList.NowProvListQuery(queryParameters);
            var nowProvs = await SendAsync(query);

            // Assert
            nowProvs
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeNowProvTwo, options =>
                    options.ExcludingMissingMembers());
            nowProvs
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeNowProvOne, options =>
                    options.ExcludingMissingMembers());
        }

        
        [Test]
        public async Task NowProvListQuery_Filters_NowProv_Id()
        {
            //Arrange
            var fakeNowProvOne = new FakeNowProv { }.Generate();
            var fakeNowProvTwo = new FakeNowProv { }.Generate();
            fakeNowProvOne.Id = Guid.NewGuid();
            fakeNowProvTwo.Id = Guid.NewGuid();
            var queryParameters = new NowProvParametersDto() { Filters = $"Id == {fakeNowProvTwo.Id}" };

            await InsertAsync(fakeNowProvOne, fakeNowProvTwo);

            //Act
            var query = new GetNowProvList.NowProvListQuery(queryParameters);
            var nowProvs = await SendAsync(query);

            // Assert
            nowProvs.Should().HaveCount(1);
            nowProvs
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeNowProvTwo, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task NowProvListQuery_Filters_NowProv_Events()
        {
            //Arrange
            var fakeNowProvOne = new FakeNowProv { }.Generate();
            var fakeNowProvTwo = new FakeNowProv { }.Generate();
            fakeNowProvOne.Events = "alpha";
            fakeNowProvTwo.Events = "bravo";
            var queryParameters = new NowProvParametersDto() { Filters = $"Events == {fakeNowProvTwo.Events}" };

            await InsertAsync(fakeNowProvOne, fakeNowProvTwo);

            //Act
            var query = new GetNowProvList.NowProvListQuery(queryParameters);
            var nowProvs = await SendAsync(query);

            // Assert
            nowProvs.Should().HaveCount(1);
            nowProvs
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeNowProvTwo, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task NowProvListQuery_Filters_NowProv_JobName()
        {
            //Arrange
            var fakeNowProvOne = new FakeNowProv { }.Generate();
            var fakeNowProvTwo = new FakeNowProv { }.Generate();
            fakeNowProvOne.JobName = "alpha";
            fakeNowProvTwo.JobName = "bravo";
            var queryParameters = new NowProvParametersDto() { Filters = $"JobName == {fakeNowProvTwo.JobName}" };

            await InsertAsync(fakeNowProvOne, fakeNowProvTwo);

            //Act
            var query = new GetNowProvList.NowProvListQuery(queryParameters);
            var nowProvs = await SendAsync(query);

            // Assert
            nowProvs.Should().HaveCount(1);
            nowProvs
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeNowProvTwo, options =>
                    options.ExcludingMissingMembers());
        }

    }
}