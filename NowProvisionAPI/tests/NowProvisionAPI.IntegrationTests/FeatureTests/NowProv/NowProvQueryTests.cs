namespace NowProvisionAPI.IntegrationTests.FeatureTests.NowProv
{
    using NowProvisionAPI.SharedTestHelpers.Fakes.NowProv;
    using NowProvisionAPI.IntegrationTests.TestUtilities;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using NowProvisionAPI.WebApi.Features.NowProvs;
    using static TestFixture;

    public class NowProvQueryTests : TestBase
    {
        [Test]
        public async Task NowProvQuery_Returns_Resource_With_Accurate_Props()
        {
            // Arrange
            var fakeNowProvOne = new FakeNowProv { }.Generate();
            await InsertAsync(fakeNowProvOne);

            // Act
            var query = new GetNowProv.NowProvQuery(fakeNowProvOne.Id);
            var nowProvs = await SendAsync(query);

            // Assert
            nowProvs.Should().BeEquivalentTo(fakeNowProvOne, options =>
                options.ExcludingMissingMembers());
        }

        [Test]
        public async Task NowProvQuery_Throws_KeyNotFoundException_When_Record_Does_Not_Exist()
        {
            // Arrange
            var badId = Guid.NewGuid();

            // Act
            var query = new GetNowProv.NowProvQuery(badId);
            Func<Task> act = () => SendAsync(query);

            // Assert
            act.Should().Throw<KeyNotFoundException>();
        }
    }
}