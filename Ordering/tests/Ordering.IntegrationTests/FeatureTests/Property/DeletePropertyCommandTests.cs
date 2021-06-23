namespace Ordering.IntegrationTests.FeatureTests.Property
{
    using Ordering.SharedTestHelpers.Fakes.Property;
    using Ordering.IntegrationTests.TestUtilities;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System;
    using System.Threading.Tasks;
    using Ordering.WebApi.Features.Propertys;
    using static TestFixture;

    public class DeletePropertyCommandTests : TestBase
    {
        [Test]
        public async Task DeletePropertyCommand_Deletes_Property_From_Db()
        {
            // Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            await InsertAsync(fakePropertyOne);
            var property = await ExecuteDbContextAsync(db => db.Propertys.SingleOrDefaultAsync());
            var propertyId = property.PropertyId;

            // Act
            var command = new DeleteProperty.DeletePropertyCommand(propertyId);
            await SendAsync(command);
            var propertys = await ExecuteDbContextAsync(db => db.Propertys.ToListAsync());

            // Assert
            propertys.Count.Should().Be(0);
        }

        [Test]
        public async Task DeletePropertyCommand_Throws_KeyNotFoundException_When_Record_Does_Not_Exist()
        {
            // Arrange
            var badId = Guid.NewGuid();

            // Act
            var command = new DeleteProperty.DeletePropertyCommand(badId);
            Func<Task> act = () => SendAsync(command);

            // Assert
            act.Should().Throw<KeyNotFoundException>();
        }
    }
}