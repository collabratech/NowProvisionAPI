namespace NowProvisionAPI.IntegrationTests.FeatureTests.Office
{
    using NowProvisionAPI.SharedTestHelpers.Fakes.Office;
    using NowProvisionAPI.IntegrationTests.TestUtilities;
    using NowProvisionAPI.Core.Dtos.Office;
    using NowProvisionAPI.Core.Exceptions;
    using NowProvisionAPI.WebApi.Features.Offices;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.JsonPatch;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using static TestFixture;

    public class PatchOfficeCommandTests : TestBase
    {
        [Test]
        public async Task PatchOfficeCommand_Updates_Existing_Office_In_Db()
        {
            // Arrange
            var fakeOfficeOne = new FakeOffice { }.Generate();
            await InsertAsync(fakeOfficeOne);
            var office = await ExecuteDbContextAsync(db => db.Offices.SingleOrDefaultAsync());
            var officeId = office.OfficeId;

            var patchDoc = new JsonPatchDocument<OfficeForUpdateDto>();
            var newValue = "Easily Identified Value For Test";
            patchDoc.Replace(o => o.Name, newValue);

            // Act
            var command = new PatchOffice.PatchOfficeCommand(officeId, patchDoc);
            await SendAsync(command);
            var updatedOffice = await ExecuteDbContextAsync(db => db.Offices.Where(o => o.OfficeId == officeId).SingleOrDefaultAsync());

            // Assert
            updatedOffice.Name.Should().Be(newValue);
        }
        
        [Test]
        public async Task PatchOfficeCommand_Throws_KeyNotFoundException_When_Bad_PK()
        {
            // Arrange
            var badId = Guid.NewGuid();
            var patchDoc = new JsonPatchDocument<OfficeForUpdateDto>();

            // Act
            var command = new PatchOffice.PatchOfficeCommand(badId, patchDoc);
            Func<Task> act = () => SendAsync(command);

            // Assert
            act.Should().Throw<KeyNotFoundException>();
        }

        [Test]
        public async Task PatchOfficeCommand_Throws_ApiException_When_Null_Patchdoc()
        {
            // Arrange
            var randomId = Guid.NewGuid();

            // Act
            var command = new PatchOffice.PatchOfficeCommand(randomId, null);
            Func<Task> act = () => SendAsync(command);

            // Assert
            act.Should().Throw<ApiException>();
        }
    }
}