namespace Ordering.IntegrationTests.FeatureTests.Property
{
    using Ordering.SharedTestHelpers.Fakes.Property;
    using Ordering.IntegrationTests.TestUtilities;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using System.Threading.Tasks;
    using Ordering.WebApi.Features.Propertys;
    using static TestFixture;
    using System;
    using Ordering.Core.Exceptions;

    public class AddPropertyCommandTests : TestBase
    {
        [Test]
        public async Task AddPropertyCommand_Adds_New_Property_To_Db()
        {
            // Arrange
            var fakePropertyOne = new FakePropertyForCreationDto { }.Generate();

            // Act
            var command = new AddProperty.AddPropertyCommand(fakePropertyOne);
            var propertyReturned = await SendAsync(command);
            var propertyCreated = await ExecuteDbContextAsync(db => db.Propertys.SingleOrDefaultAsync());

            // Assert
            propertyReturned.Should().BeEquivalentTo(fakePropertyOne, options =>
                options.ExcludingMissingMembers());
            propertyCreated.Should().BeEquivalentTo(fakePropertyOne, options =>
                options.ExcludingMissingMembers());
        }

        [Test]
        public async Task AddPropertyCommand_Throws_Conflict_When_PK_Guid_Exists()
        {
            // Arrange
            var FakeProperty = new FakeProperty { }.Generate();
            var conflictRecord = new FakePropertyForCreationDto { }.Generate();
            conflictRecord.PropertyId = FakeProperty.PropertyId;

            await InsertAsync(FakeProperty);

            // Act
            var command = new AddProperty.AddPropertyCommand(conflictRecord);
            Func<Task> act = () => SendAsync(command);

            // Assert
            act.Should().Throw<ConflictException>();
        }
    }
}