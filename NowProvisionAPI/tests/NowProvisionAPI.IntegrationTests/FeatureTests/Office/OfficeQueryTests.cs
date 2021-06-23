namespace NowProvisionAPI.IntegrationTests.FeatureTests.Office
{
    using NowProvisionAPI.SharedTestHelpers.Fakes.Office;
    using NowProvisionAPI.IntegrationTests.TestUtilities;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using NowProvisionAPI.WebApi.Features.Offices;
    using static TestFixture;

    public class OfficeQueryTests : TestBase
    {
        [Test]
        public async Task OfficeQuery_Returns_Resource_With_Accurate_Props()
        {
            // Arrange
            var fakeOfficeOne = new FakeOffice { }.Generate();
            await InsertAsync(fakeOfficeOne);

            // Act
            var query = new GetOffice.OfficeQuery(fakeOfficeOne.OfficeId);
            var offices = await SendAsync(query);

            // Assert
            offices.Should().BeEquivalentTo(fakeOfficeOne, options =>
                options.ExcludingMissingMembers());
        }

        [Test]
        public async Task OfficeQuery_Throws_KeyNotFoundException_When_Record_Does_Not_Exist()
        {
            // Arrange
            var badId = Guid.NewGuid();

            // Act
            var query = new GetOffice.OfficeQuery(badId);
            Func<Task> act = () => SendAsync(query);

            // Assert
            act.Should().Throw<KeyNotFoundException>();
        }
    }
}