namespace NowProvisionAPI.IntegrationTests.FeatureTests.Property
{
    using NowProvisionAPI.SharedTestHelpers.Fakes.Property;
    using NowProvisionAPI.IntegrationTests.TestUtilities;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using NowProvisionAPI.WebApi.Features.Propertys;
    using static TestFixture;

    public class PropertyQueryTests : TestBase
    {
        [Test]
        public async Task PropertyQuery_Returns_Resource_With_Accurate_Props()
        {
            // Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            await InsertAsync(fakePropertyOne);

            // Act
            var query = new GetProperty.PropertyQuery(fakePropertyOne.PropertyId);
            var propertys = await SendAsync(query);

            // Assert
            propertys.Should().BeEquivalentTo(fakePropertyOne, options =>
                options.ExcludingMissingMembers());
        }

        [Test]
        public async Task PropertyQuery_Throws_KeyNotFoundException_When_Record_Does_Not_Exist()
        {
            // Arrange
            var badId = Guid.NewGuid();

            // Act
            var query = new GetProperty.PropertyQuery(badId);
            Func<Task> act = () => SendAsync(query);

            // Assert
            act.Should().Throw<KeyNotFoundException>();
        }
    }
}