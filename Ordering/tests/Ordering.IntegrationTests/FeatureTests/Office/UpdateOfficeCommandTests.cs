namespace Ordering.IntegrationTests.FeatureTests.Office
{
    using Ordering.SharedTestHelpers.Fakes.Office;
    using Ordering.IntegrationTests.TestUtilities;
    using Ordering.Core.Dtos.Office;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.JsonPatch;
    using System.Linq;
    using Ordering.WebApi.Features.Offices;
    using static TestFixture;

    public class UpdateOfficeCommandTests : TestBase
    {
        [Test]
        public async Task UpdateOfficeCommand_Updates_Existing_Office_In_Db()
        {
            // Arrange
            var fakeOfficeOne = new FakeOffice { }.Generate();
            var updatedOfficeDto = new FakeOfficeForUpdateDto { }.Generate();
            await InsertAsync(fakeOfficeOne);

            var office = await ExecuteDbContextAsync(db => db.Offices.SingleOrDefaultAsync());
            var officeId = office.OfficeId;

            // Act
            var command = new UpdateOffice.UpdateOfficeCommand(officeId, updatedOfficeDto);
            await SendAsync(command);
            var updatedOffice = await ExecuteDbContextAsync(db => db.Offices.Where(o => o.OfficeId == officeId).SingleOrDefaultAsync());

            // Assert
            updatedOffice.Should().BeEquivalentTo(updatedOfficeDto, options =>
                options.ExcludingMissingMembers());
        }
    }
}