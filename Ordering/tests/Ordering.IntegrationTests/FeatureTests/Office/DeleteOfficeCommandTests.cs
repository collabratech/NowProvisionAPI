namespace Ordering.IntegrationTests.FeatureTests.Office
{
    using Ordering.SharedTestHelpers.Fakes.Office;
    using Ordering.IntegrationTests.TestUtilities;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System;
    using System.Threading.Tasks;
    using Ordering.WebApi.Features.Offices;
    using static TestFixture;

    public class DeleteOfficeCommandTests : TestBase
    {
        [Test]
        public async Task DeleteOfficeCommand_Deletes_Office_From_Db()
        {
            // Arrange
            var fakeOfficeOne = new FakeOffice { }.Generate();
            await InsertAsync(fakeOfficeOne);
            var office = await ExecuteDbContextAsync(db => db.Offices.SingleOrDefaultAsync());
            var officeId = office.OfficeId;

            // Act
            var command = new DeleteOffice.DeleteOfficeCommand(officeId);
            await SendAsync(command);
            var offices = await ExecuteDbContextAsync(db => db.Offices.ToListAsync());

            // Assert
            offices.Count.Should().Be(0);
        }

        [Test]
        public async Task DeleteOfficeCommand_Throws_KeyNotFoundException_When_Record_Does_Not_Exist()
        {
            // Arrange
            var badId = Guid.NewGuid();

            // Act
            var command = new DeleteOffice.DeleteOfficeCommand(badId);
            Func<Task> act = () => SendAsync(command);

            // Assert
            act.Should().Throw<KeyNotFoundException>();
        }
    }
}