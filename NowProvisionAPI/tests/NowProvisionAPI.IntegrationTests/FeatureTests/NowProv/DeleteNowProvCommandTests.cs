namespace NowProvisionAPI.IntegrationTests.FeatureTests.NowProv
{
    using NowProvisionAPI.SharedTestHelpers.Fakes.NowProv;
    using NowProvisionAPI.IntegrationTests.TestUtilities;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System;
    using System.Threading.Tasks;
    using NowProvisionAPI.WebApi.Features.NowProvs;
    using static TestFixture;

    public class DeleteNowProvCommandTests : TestBase
    {
        [Test]
        public async Task DeleteNowProvCommand_Deletes_NowProv_From_Db()
        {
            // Arrange
            var fakeNowProvOne = new FakeNowProv { }.Generate();
            await InsertAsync(fakeNowProvOne);
            var nowProv = await ExecuteDbContextAsync(db => db.NowProvs.SingleOrDefaultAsync());
            var id = nowProv.Id;

            // Act
            var command = new DeleteNowProv.DeleteNowProvCommand(id);
            await SendAsync(command);
            var nowProvs = await ExecuteDbContextAsync(db => db.NowProvs.ToListAsync());

            // Assert
            nowProvs.Count.Should().Be(0);
        }

        [Test]
        public async Task DeleteNowProvCommand_Throws_KeyNotFoundException_When_Record_Does_Not_Exist()
        {
            // Arrange
            var badId = Guid.NewGuid();

            // Act
            var command = new DeleteNowProv.DeleteNowProvCommand(badId);
            Func<Task> act = () => SendAsync(command);

            // Assert
            act.Should().Throw<KeyNotFoundException>();
        }
    }
}