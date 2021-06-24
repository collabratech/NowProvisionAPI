namespace NowProvisionAPI.IntegrationTests.FeatureTests.NowProv
{
    using NowProvisionAPI.SharedTestHelpers.Fakes.NowProv;
    using NowProvisionAPI.IntegrationTests.TestUtilities;
    using NowProvisionAPI.Core.Dtos.NowProv;
    using NowProvisionAPI.Core.Exceptions;
    using NowProvisionAPI.WebApi.Features.NowProvs;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.JsonPatch;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using static TestFixture;

    public class PatchNowProvCommandTests : TestBase
    {
        [Test]
        public async Task PatchNowProvCommand_Updates_Existing_NowProv_In_Db()
        {
            // Arrange
            var fakeNowProvOne = new FakeNowProv { }.Generate();
            await InsertAsync(fakeNowProvOne);
            var nowProv = await ExecuteDbContextAsync(db => db.NowProvs.SingleOrDefaultAsync());
            var id = nowProv.Id;

            var patchDoc = new JsonPatchDocument<NowProvForUpdateDto>();
            var newValue = "Easily Identified Value For Test";
            patchDoc.Replace(n => n.Events, newValue);

            // Act
            var command = new PatchNowProv.PatchNowProvCommand(id, patchDoc);
            await SendAsync(command);
            var updatedNowProv = await ExecuteDbContextAsync(db => db.NowProvs.Where(n => n.Id == id).SingleOrDefaultAsync());

            // Assert
            updatedNowProv.Events.Should().Be(newValue);
        }
        
        [Test]
        public async Task PatchNowProvCommand_Throws_KeyNotFoundException_When_Bad_PK()
        {
            // Arrange
            var badId = Guid.NewGuid();
            var patchDoc = new JsonPatchDocument<NowProvForUpdateDto>();

            // Act
            var command = new PatchNowProv.PatchNowProvCommand(badId, patchDoc);
            Func<Task> act = () => SendAsync(command);

            // Assert
            act.Should().Throw<KeyNotFoundException>();
        }

        [Test]
        public async Task PatchNowProvCommand_Throws_ApiException_When_Null_Patchdoc()
        {
            // Arrange
            var randomId = Guid.NewGuid();

            // Act
            var command = new PatchNowProv.PatchNowProvCommand(randomId, null);
            Func<Task> act = () => SendAsync(command);

            // Assert
            act.Should().Throw<ApiException>();
        }
    }
}