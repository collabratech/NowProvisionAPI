namespace Ordering.IntegrationTests.FeatureTests.Office
{
    using Ordering.SharedTestHelpers.Fakes.Office;
    using Ordering.IntegrationTests.TestUtilities;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using System.Threading.Tasks;
    using Ordering.WebApi.Features.Offices;
    using static TestFixture;
    using System;
    using Ordering.Core.Exceptions;

    public class AddOfficeCommandTests : TestBase
    {
        [Test]
        public async Task AddOfficeCommand_Adds_New_Office_To_Db()
        {
            // Arrange
            var fakeOfficeOne = new FakeOfficeForCreationDto { }.Generate();

            // Act
            var command = new AddOffice.AddOfficeCommand(fakeOfficeOne);
            var officeReturned = await SendAsync(command);
            var officeCreated = await ExecuteDbContextAsync(db => db.Offices.SingleOrDefaultAsync());

            // Assert
            officeReturned.Should().BeEquivalentTo(fakeOfficeOne, options =>
                options.ExcludingMissingMembers());
            officeCreated.Should().BeEquivalentTo(fakeOfficeOne, options =>
                options.ExcludingMissingMembers());
        }

        [Test]
        public async Task AddOfficeCommand_Throws_Conflict_When_PK_Guid_Exists()
        {
            // Arrange
            var FakeOffice = new FakeOffice { }.Generate();
            var conflictRecord = new FakeOfficeForCreationDto { }.Generate();
            conflictRecord.OfficeId = FakeOffice.OfficeId;

            await InsertAsync(FakeOffice);

            // Act
            var command = new AddOffice.AddOfficeCommand(conflictRecord);
            Func<Task> act = () => SendAsync(command);

            // Assert
            act.Should().Throw<ConflictException>();
        }
    }
}