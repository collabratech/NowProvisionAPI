namespace NowProvisionAPI.IntegrationTests.FeatureTests.NowProv
{
    using NowProvisionAPI.SharedTestHelpers.Fakes.NowProv;
    using NowProvisionAPI.IntegrationTests.TestUtilities;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using System.Threading.Tasks;
    using NowProvisionAPI.WebApi.Features.NowProvs;
    using static TestFixture;
    using System;
    using NowProvisionAPI.Core.Exceptions;

    public class AddNowProvCommandTests : TestBase
    {
        [Test]
        public async Task AddNowProvCommand_Adds_New_NowProv_To_Db()
        {
            // Arrange
            var fakeNowProvOne = new FakeNowProvForCreationDto { }.Generate();

            // Act
            var command = new AddNowProv.AddNowProvCommand(fakeNowProvOne);
            var nowProvReturned = await SendAsync(command);
            var nowProvCreated = await ExecuteDbContextAsync(db => db.NowProvs.SingleOrDefaultAsync());

            // Assert
            nowProvReturned.Should().BeEquivalentTo(fakeNowProvOne, options =>
                options.ExcludingMissingMembers());
            nowProvCreated.Should().BeEquivalentTo(fakeNowProvOne, options =>
                options.ExcludingMissingMembers());
        }

        [Test]
        public async Task AddNowProvCommand_Throws_Conflict_When_PK_Guid_Exists()
        {
            // Arrange
            var FakeNowProv = new FakeNowProv { }.Generate();
            var conflictRecord = new FakeNowProvForCreationDto { }.Generate();
            conflictRecord.Id = FakeNowProv.Id;

            await InsertAsync(FakeNowProv);

            // Act
            var command = new AddNowProv.AddNowProvCommand(conflictRecord);
            Func<Task> act = () => SendAsync(command);

            // Assert
            act.Should().Throw<ConflictException>();
        }
    }
}