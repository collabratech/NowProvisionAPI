namespace NowProvisionAPI.IntegrationTests.FeatureTests.NowProv
{
    using NowProvisionAPI.SharedTestHelpers.Fakes.NowProv;
    using NowProvisionAPI.IntegrationTests.TestUtilities;
    using NowProvisionAPI.Core.Dtos.NowProv;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.JsonPatch;
    using System.Linq;
    using NowProvisionAPI.WebApi.Features.NowProvs;
    using static TestFixture;

    public class UpdateNowProvCommandTests : TestBase
    {
        [Test]
        public async Task UpdateNowProvCommand_Updates_Existing_NowProv_In_Db()
        {
            // Arrange
            var fakeNowProvOne = new FakeNowProv { }.Generate();
            var updatedNowProvDto = new FakeNowProvForUpdateDto { }.Generate();
            await InsertAsync(fakeNowProvOne);

            var nowProv = await ExecuteDbContextAsync(db => db.NowProvs.SingleOrDefaultAsync());
            var id = nowProv.Id;

            // Act
            var command = new UpdateNowProv.UpdateNowProvCommand(id, updatedNowProvDto);
            await SendAsync(command);
            var updatedNowProv = await ExecuteDbContextAsync(db => db.NowProvs.Where(n => n.Id == id).SingleOrDefaultAsync());

            // Assert
            updatedNowProv.Should().BeEquivalentTo(updatedNowProvDto, options =>
                options.ExcludingMissingMembers());
        }
    }
}