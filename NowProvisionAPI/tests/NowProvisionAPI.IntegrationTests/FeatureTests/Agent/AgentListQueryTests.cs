namespace NowProvisionAPI.IntegrationTests.FeatureTests.Agent
{
    using NowProvisionAPI.Core.Dtos.Agent;
    using NowProvisionAPI.SharedTestHelpers.Fakes.Agent;
    using NowProvisionAPI.Core.Exceptions;
    using NowProvisionAPI.WebApi.Features.Agents;
    using FluentAssertions;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using static TestFixture;

    public class AgentListQueryTests : TestBase
    {
        
        [Test]
        public async Task AgentListQuery_Returns_Resource_With_Accurate_Props()
        {
            // Arrange
            var fakeAgentOne = new FakeAgent { }.Generate();
            var fakeAgentTwo = new FakeAgent { }.Generate();
            var queryParameters = new AgentParametersDto();

            await InsertAsync(fakeAgentOne, fakeAgentTwo);

            // Act
            var query = new GetAgentList.AgentListQuery(queryParameters);
            var agents = await SendAsync(query);

            // Assert
            agents.Should().HaveCount(2);
        }
        
        [Test]
        public async Task AgentListQuery_Returns_Expected_Page_Size_And_Number()
        {
            //Arrange
            var fakeAgentOne = new FakeAgent { }.Generate();
            var fakeAgentTwo = new FakeAgent { }.Generate();
            var fakeAgentThree = new FakeAgent { }.Generate();
            var queryParameters = new AgentParametersDto() { PageSize = 1, PageNumber = 2 };

            await InsertAsync(fakeAgentOne, fakeAgentTwo, fakeAgentThree);

            //Act
            var query = new GetAgentList.AgentListQuery(queryParameters);
            var agents = await SendAsync(query);

            // Assert
            agents.Should().HaveCount(1);
        }
        
        [Test]
        public async Task AgentListQuery_Throws_ApiException_When_Null_Query_Parameters()
        {
            // Arrange
            // N/A

            // Act
            var query = new GetAgentList.AgentListQuery(null);
            Func<Task> act = () => SendAsync(query);

            // Assert
            act.Should().Throw<ApiException>();
        }
        
        [Test]
        public async Task AgentListQuery_Returns_Sorted_Agent_Name_List_In_Asc_Order()
        {
            //Arrange
            var fakeAgentOne = new FakeAgent { }.Generate();
            var fakeAgentTwo = new FakeAgent { }.Generate();
            fakeAgentOne.Name = "bravo";
            fakeAgentTwo.Name = "alpha";
            var queryParameters = new AgentParametersDto() { SortOrder = "Name" };

            await InsertAsync(fakeAgentOne, fakeAgentTwo);

            //Act
            var query = new GetAgentList.AgentListQuery(queryParameters);
            var agents = await SendAsync(query);

            // Assert
            agents
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeAgentTwo, options =>
                    options.ExcludingMissingMembers());
            agents
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeAgentOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task AgentListQuery_Returns_Sorted_Agent_Name_List_In_Desc_Order()
        {
            //Arrange
            var fakeAgentOne = new FakeAgent { }.Generate();
            var fakeAgentTwo = new FakeAgent { }.Generate();
            fakeAgentOne.Name = "bravo";
            fakeAgentTwo.Name = "alpha";
            var queryParameters = new AgentParametersDto() { SortOrder = "Name" };

            await InsertAsync(fakeAgentOne, fakeAgentTwo);

            //Act
            var query = new GetAgentList.AgentListQuery(queryParameters);
            var agents = await SendAsync(query);

            // Assert
            agents
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeAgentTwo, options =>
                    options.ExcludingMissingMembers());
            agents
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeAgentOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task AgentListQuery_Returns_Sorted_Agent_Phone_List_In_Asc_Order()
        {
            //Arrange
            var fakeAgentOne = new FakeAgent { }.Generate();
            var fakeAgentTwo = new FakeAgent { }.Generate();
            fakeAgentOne.Phone = "bravo";
            fakeAgentTwo.Phone = "alpha";
            var queryParameters = new AgentParametersDto() { SortOrder = "Phone" };

            await InsertAsync(fakeAgentOne, fakeAgentTwo);

            //Act
            var query = new GetAgentList.AgentListQuery(queryParameters);
            var agents = await SendAsync(query);

            // Assert
            agents
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeAgentTwo, options =>
                    options.ExcludingMissingMembers());
            agents
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeAgentOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task AgentListQuery_Returns_Sorted_Agent_Phone_List_In_Desc_Order()
        {
            //Arrange
            var fakeAgentOne = new FakeAgent { }.Generate();
            var fakeAgentTwo = new FakeAgent { }.Generate();
            fakeAgentOne.Phone = "bravo";
            fakeAgentTwo.Phone = "alpha";
            var queryParameters = new AgentParametersDto() { SortOrder = "Phone" };

            await InsertAsync(fakeAgentOne, fakeAgentTwo);

            //Act
            var query = new GetAgentList.AgentListQuery(queryParameters);
            var agents = await SendAsync(query);

            // Assert
            agents
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeAgentTwo, options =>
                    options.ExcludingMissingMembers());
            agents
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeAgentOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task AgentListQuery_Returns_Sorted_Agent_Email_List_In_Asc_Order()
        {
            //Arrange
            var fakeAgentOne = new FakeAgent { }.Generate();
            var fakeAgentTwo = new FakeAgent { }.Generate();
            fakeAgentOne.Email = "bravo";
            fakeAgentTwo.Email = "alpha";
            var queryParameters = new AgentParametersDto() { SortOrder = "Email" };

            await InsertAsync(fakeAgentOne, fakeAgentTwo);

            //Act
            var query = new GetAgentList.AgentListQuery(queryParameters);
            var agents = await SendAsync(query);

            // Assert
            agents
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeAgentTwo, options =>
                    options.ExcludingMissingMembers());
            agents
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeAgentOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task AgentListQuery_Returns_Sorted_Agent_Email_List_In_Desc_Order()
        {
            //Arrange
            var fakeAgentOne = new FakeAgent { }.Generate();
            var fakeAgentTwo = new FakeAgent { }.Generate();
            fakeAgentOne.Email = "bravo";
            fakeAgentTwo.Email = "alpha";
            var queryParameters = new AgentParametersDto() { SortOrder = "Email" };

            await InsertAsync(fakeAgentOne, fakeAgentTwo);

            //Act
            var query = new GetAgentList.AgentListQuery(queryParameters);
            var agents = await SendAsync(query);

            // Assert
            agents
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeAgentTwo, options =>
                    options.ExcludingMissingMembers());
            agents
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeAgentOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task AgentListQuery_Returns_Sorted_Agent_Website_List_In_Asc_Order()
        {
            //Arrange
            var fakeAgentOne = new FakeAgent { }.Generate();
            var fakeAgentTwo = new FakeAgent { }.Generate();
            fakeAgentOne.Website = "bravo";
            fakeAgentTwo.Website = "alpha";
            var queryParameters = new AgentParametersDto() { SortOrder = "Website" };

            await InsertAsync(fakeAgentOne, fakeAgentTwo);

            //Act
            var query = new GetAgentList.AgentListQuery(queryParameters);
            var agents = await SendAsync(query);

            // Assert
            agents
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeAgentTwo, options =>
                    options.ExcludingMissingMembers());
            agents
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeAgentOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task AgentListQuery_Returns_Sorted_Agent_Website_List_In_Desc_Order()
        {
            //Arrange
            var fakeAgentOne = new FakeAgent { }.Generate();
            var fakeAgentTwo = new FakeAgent { }.Generate();
            fakeAgentOne.Website = "bravo";
            fakeAgentTwo.Website = "alpha";
            var queryParameters = new AgentParametersDto() { SortOrder = "Website" };

            await InsertAsync(fakeAgentOne, fakeAgentTwo);

            //Act
            var query = new GetAgentList.AgentListQuery(queryParameters);
            var agents = await SendAsync(query);

            // Assert
            agents
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeAgentTwo, options =>
                    options.ExcludingMissingMembers());
            agents
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeAgentOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task AgentListQuery_Returns_Sorted_Agent_Twitter_List_In_Asc_Order()
        {
            //Arrange
            var fakeAgentOne = new FakeAgent { }.Generate();
            var fakeAgentTwo = new FakeAgent { }.Generate();
            fakeAgentOne.Twitter = "bravo";
            fakeAgentTwo.Twitter = "alpha";
            var queryParameters = new AgentParametersDto() { SortOrder = "Twitter" };

            await InsertAsync(fakeAgentOne, fakeAgentTwo);

            //Act
            var query = new GetAgentList.AgentListQuery(queryParameters);
            var agents = await SendAsync(query);

            // Assert
            agents
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeAgentTwo, options =>
                    options.ExcludingMissingMembers());
            agents
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeAgentOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task AgentListQuery_Returns_Sorted_Agent_Twitter_List_In_Desc_Order()
        {
            //Arrange
            var fakeAgentOne = new FakeAgent { }.Generate();
            var fakeAgentTwo = new FakeAgent { }.Generate();
            fakeAgentOne.Twitter = "bravo";
            fakeAgentTwo.Twitter = "alpha";
            var queryParameters = new AgentParametersDto() { SortOrder = "Twitter" };

            await InsertAsync(fakeAgentOne, fakeAgentTwo);

            //Act
            var query = new GetAgentList.AgentListQuery(queryParameters);
            var agents = await SendAsync(query);

            // Assert
            agents
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeAgentTwo, options =>
                    options.ExcludingMissingMembers());
            agents
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeAgentOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task AgentListQuery_Returns_Sorted_Agent_Facebook_List_In_Asc_Order()
        {
            //Arrange
            var fakeAgentOne = new FakeAgent { }.Generate();
            var fakeAgentTwo = new FakeAgent { }.Generate();
            fakeAgentOne.Facebook = "bravo";
            fakeAgentTwo.Facebook = "alpha";
            var queryParameters = new AgentParametersDto() { SortOrder = "Facebook" };

            await InsertAsync(fakeAgentOne, fakeAgentTwo);

            //Act
            var query = new GetAgentList.AgentListQuery(queryParameters);
            var agents = await SendAsync(query);

            // Assert
            agents
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeAgentTwo, options =>
                    options.ExcludingMissingMembers());
            agents
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeAgentOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task AgentListQuery_Returns_Sorted_Agent_Facebook_List_In_Desc_Order()
        {
            //Arrange
            var fakeAgentOne = new FakeAgent { }.Generate();
            var fakeAgentTwo = new FakeAgent { }.Generate();
            fakeAgentOne.Facebook = "bravo";
            fakeAgentTwo.Facebook = "alpha";
            var queryParameters = new AgentParametersDto() { SortOrder = "Facebook" };

            await InsertAsync(fakeAgentOne, fakeAgentTwo);

            //Act
            var query = new GetAgentList.AgentListQuery(queryParameters);
            var agents = await SendAsync(query);

            // Assert
            agents
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeAgentTwo, options =>
                    options.ExcludingMissingMembers());
            agents
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeAgentOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task AgentListQuery_Returns_Sorted_Agent_LinkedIn_List_In_Asc_Order()
        {
            //Arrange
            var fakeAgentOne = new FakeAgent { }.Generate();
            var fakeAgentTwo = new FakeAgent { }.Generate();
            fakeAgentOne.LinkedIn = "bravo";
            fakeAgentTwo.LinkedIn = "alpha";
            var queryParameters = new AgentParametersDto() { SortOrder = "LinkedIn" };

            await InsertAsync(fakeAgentOne, fakeAgentTwo);

            //Act
            var query = new GetAgentList.AgentListQuery(queryParameters);
            var agents = await SendAsync(query);

            // Assert
            agents
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeAgentTwo, options =>
                    options.ExcludingMissingMembers());
            agents
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeAgentOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task AgentListQuery_Returns_Sorted_Agent_LinkedIn_List_In_Desc_Order()
        {
            //Arrange
            var fakeAgentOne = new FakeAgent { }.Generate();
            var fakeAgentTwo = new FakeAgent { }.Generate();
            fakeAgentOne.LinkedIn = "bravo";
            fakeAgentTwo.LinkedIn = "alpha";
            var queryParameters = new AgentParametersDto() { SortOrder = "LinkedIn" };

            await InsertAsync(fakeAgentOne, fakeAgentTwo);

            //Act
            var query = new GetAgentList.AgentListQuery(queryParameters);
            var agents = await SendAsync(query);

            // Assert
            agents
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeAgentTwo, options =>
                    options.ExcludingMissingMembers());
            agents
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeAgentOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task AgentListQuery_Returns_Sorted_Agent_License_List_In_Asc_Order()
        {
            //Arrange
            var fakeAgentOne = new FakeAgent { }.Generate();
            var fakeAgentTwo = new FakeAgent { }.Generate();
            fakeAgentOne.License = "bravo";
            fakeAgentTwo.License = "alpha";
            var queryParameters = new AgentParametersDto() { SortOrder = "License" };

            await InsertAsync(fakeAgentOne, fakeAgentTwo);

            //Act
            var query = new GetAgentList.AgentListQuery(queryParameters);
            var agents = await SendAsync(query);

            // Assert
            agents
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeAgentTwo, options =>
                    options.ExcludingMissingMembers());
            agents
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeAgentOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task AgentListQuery_Returns_Sorted_Agent_License_List_In_Desc_Order()
        {
            //Arrange
            var fakeAgentOne = new FakeAgent { }.Generate();
            var fakeAgentTwo = new FakeAgent { }.Generate();
            fakeAgentOne.License = "bravo";
            fakeAgentTwo.License = "alpha";
            var queryParameters = new AgentParametersDto() { SortOrder = "License" };

            await InsertAsync(fakeAgentOne, fakeAgentTwo);

            //Act
            var query = new GetAgentList.AgentListQuery(queryParameters);
            var agents = await SendAsync(query);

            // Assert
            agents
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeAgentTwo, options =>
                    options.ExcludingMissingMembers());
            agents
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeAgentOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task AgentListQuery_Returns_Sorted_Agent_LicenseIcon_List_In_Asc_Order()
        {
            //Arrange
            var fakeAgentOne = new FakeAgent { }.Generate();
            var fakeAgentTwo = new FakeAgent { }.Generate();
            fakeAgentOne.LicenseIcon = "bravo";
            fakeAgentTwo.LicenseIcon = "alpha";
            var queryParameters = new AgentParametersDto() { SortOrder = "LicenseIcon" };

            await InsertAsync(fakeAgentOne, fakeAgentTwo);

            //Act
            var query = new GetAgentList.AgentListQuery(queryParameters);
            var agents = await SendAsync(query);

            // Assert
            agents
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeAgentTwo, options =>
                    options.ExcludingMissingMembers());
            agents
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeAgentOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task AgentListQuery_Returns_Sorted_Agent_LicenseIcon_List_In_Desc_Order()
        {
            //Arrange
            var fakeAgentOne = new FakeAgent { }.Generate();
            var fakeAgentTwo = new FakeAgent { }.Generate();
            fakeAgentOne.LicenseIcon = "bravo";
            fakeAgentTwo.LicenseIcon = "alpha";
            var queryParameters = new AgentParametersDto() { SortOrder = "LicenseIcon" };

            await InsertAsync(fakeAgentOne, fakeAgentTwo);

            //Act
            var query = new GetAgentList.AgentListQuery(queryParameters);
            var agents = await SendAsync(query);

            // Assert
            agents
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeAgentTwo, options =>
                    options.ExcludingMissingMembers());
            agents
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeAgentOne, options =>
                    options.ExcludingMissingMembers());
        }

        
        [Test]
        public async Task AgentListQuery_Filters_Agent_AgentId()
        {
            //Arrange
            var fakeAgentOne = new FakeAgent { }.Generate();
            var fakeAgentTwo = new FakeAgent { }.Generate();
            fakeAgentOne.AgentId = Guid.NewGuid();
            fakeAgentTwo.AgentId = Guid.NewGuid();
            var queryParameters = new AgentParametersDto() { Filters = $"AgentId == {fakeAgentTwo.AgentId}" };

            await InsertAsync(fakeAgentOne, fakeAgentTwo);

            //Act
            var query = new GetAgentList.AgentListQuery(queryParameters);
            var agents = await SendAsync(query);

            // Assert
            agents.Should().HaveCount(1);
            agents
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeAgentTwo, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task AgentListQuery_Filters_Agent_Name()
        {
            //Arrange
            var fakeAgentOne = new FakeAgent { }.Generate();
            var fakeAgentTwo = new FakeAgent { }.Generate();
            fakeAgentOne.Name = "alpha";
            fakeAgentTwo.Name = "bravo";
            var queryParameters = new AgentParametersDto() { Filters = $"Name == {fakeAgentTwo.Name}" };

            await InsertAsync(fakeAgentOne, fakeAgentTwo);

            //Act
            var query = new GetAgentList.AgentListQuery(queryParameters);
            var agents = await SendAsync(query);

            // Assert
            agents.Should().HaveCount(1);
            agents
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeAgentTwo, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task AgentListQuery_Filters_Agent_Phone()
        {
            //Arrange
            var fakeAgentOne = new FakeAgent { }.Generate();
            var fakeAgentTwo = new FakeAgent { }.Generate();
            fakeAgentOne.Phone = "alpha";
            fakeAgentTwo.Phone = "bravo";
            var queryParameters = new AgentParametersDto() { Filters = $"Phone == {fakeAgentTwo.Phone}" };

            await InsertAsync(fakeAgentOne, fakeAgentTwo);

            //Act
            var query = new GetAgentList.AgentListQuery(queryParameters);
            var agents = await SendAsync(query);

            // Assert
            agents.Should().HaveCount(1);
            agents
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeAgentTwo, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task AgentListQuery_Filters_Agent_Email()
        {
            //Arrange
            var fakeAgentOne = new FakeAgent { }.Generate();
            var fakeAgentTwo = new FakeAgent { }.Generate();
            fakeAgentOne.Email = "alpha";
            fakeAgentTwo.Email = "bravo";
            var queryParameters = new AgentParametersDto() { Filters = $"Email == {fakeAgentTwo.Email}" };

            await InsertAsync(fakeAgentOne, fakeAgentTwo);

            //Act
            var query = new GetAgentList.AgentListQuery(queryParameters);
            var agents = await SendAsync(query);

            // Assert
            agents.Should().HaveCount(1);
            agents
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeAgentTwo, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task AgentListQuery_Filters_Agent_Website()
        {
            //Arrange
            var fakeAgentOne = new FakeAgent { }.Generate();
            var fakeAgentTwo = new FakeAgent { }.Generate();
            fakeAgentOne.Website = "alpha";
            fakeAgentTwo.Website = "bravo";
            var queryParameters = new AgentParametersDto() { Filters = $"Website == {fakeAgentTwo.Website}" };

            await InsertAsync(fakeAgentOne, fakeAgentTwo);

            //Act
            var query = new GetAgentList.AgentListQuery(queryParameters);
            var agents = await SendAsync(query);

            // Assert
            agents.Should().HaveCount(1);
            agents
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeAgentTwo, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task AgentListQuery_Filters_Agent_Twitter()
        {
            //Arrange
            var fakeAgentOne = new FakeAgent { }.Generate();
            var fakeAgentTwo = new FakeAgent { }.Generate();
            fakeAgentOne.Twitter = "alpha";
            fakeAgentTwo.Twitter = "bravo";
            var queryParameters = new AgentParametersDto() { Filters = $"Twitter == {fakeAgentTwo.Twitter}" };

            await InsertAsync(fakeAgentOne, fakeAgentTwo);

            //Act
            var query = new GetAgentList.AgentListQuery(queryParameters);
            var agents = await SendAsync(query);

            // Assert
            agents.Should().HaveCount(1);
            agents
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeAgentTwo, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task AgentListQuery_Filters_Agent_Facebook()
        {
            //Arrange
            var fakeAgentOne = new FakeAgent { }.Generate();
            var fakeAgentTwo = new FakeAgent { }.Generate();
            fakeAgentOne.Facebook = "alpha";
            fakeAgentTwo.Facebook = "bravo";
            var queryParameters = new AgentParametersDto() { Filters = $"Facebook == {fakeAgentTwo.Facebook}" };

            await InsertAsync(fakeAgentOne, fakeAgentTwo);

            //Act
            var query = new GetAgentList.AgentListQuery(queryParameters);
            var agents = await SendAsync(query);

            // Assert
            agents.Should().HaveCount(1);
            agents
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeAgentTwo, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task AgentListQuery_Filters_Agent_LinkedIn()
        {
            //Arrange
            var fakeAgentOne = new FakeAgent { }.Generate();
            var fakeAgentTwo = new FakeAgent { }.Generate();
            fakeAgentOne.LinkedIn = "alpha";
            fakeAgentTwo.LinkedIn = "bravo";
            var queryParameters = new AgentParametersDto() { Filters = $"LinkedIn == {fakeAgentTwo.LinkedIn}" };

            await InsertAsync(fakeAgentOne, fakeAgentTwo);

            //Act
            var query = new GetAgentList.AgentListQuery(queryParameters);
            var agents = await SendAsync(query);

            // Assert
            agents.Should().HaveCount(1);
            agents
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeAgentTwo, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task AgentListQuery_Filters_Agent_License()
        {
            //Arrange
            var fakeAgentOne = new FakeAgent { }.Generate();
            var fakeAgentTwo = new FakeAgent { }.Generate();
            fakeAgentOne.License = "alpha";
            fakeAgentTwo.License = "bravo";
            var queryParameters = new AgentParametersDto() { Filters = $"License == {fakeAgentTwo.License}" };

            await InsertAsync(fakeAgentOne, fakeAgentTwo);

            //Act
            var query = new GetAgentList.AgentListQuery(queryParameters);
            var agents = await SendAsync(query);

            // Assert
            agents.Should().HaveCount(1);
            agents
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeAgentTwo, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task AgentListQuery_Filters_Agent_LicenseIcon()
        {
            //Arrange
            var fakeAgentOne = new FakeAgent { }.Generate();
            var fakeAgentTwo = new FakeAgent { }.Generate();
            fakeAgentOne.LicenseIcon = "alpha";
            fakeAgentTwo.LicenseIcon = "bravo";
            var queryParameters = new AgentParametersDto() { Filters = $"LicenseIcon == {fakeAgentTwo.LicenseIcon}" };

            await InsertAsync(fakeAgentOne, fakeAgentTwo);

            //Act
            var query = new GetAgentList.AgentListQuery(queryParameters);
            var agents = await SendAsync(query);

            // Assert
            agents.Should().HaveCount(1);
            agents
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeAgentTwo, options =>
                    options.ExcludingMissingMembers());
        }

    }
}