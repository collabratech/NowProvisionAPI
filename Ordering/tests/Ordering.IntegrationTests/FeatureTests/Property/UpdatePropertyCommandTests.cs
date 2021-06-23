namespace Ordering.IntegrationTests.FeatureTests.Property
{
    using Ordering.SharedTestHelpers.Fakes.Property;
    using Ordering.IntegrationTests.TestUtilities;
    using Ordering.Core.Dtos.Property;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.JsonPatch;
    using System.Linq;
    using Ordering.WebApi.Features.Propertys;
    using static TestFixture;

    public class UpdatePropertyCommandTests : TestBase
    {
        [Test]
        public async Task UpdatePropertyCommand_Updates_Existing_Property_In_Db()
        {
            // Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            var updatedPropertyDto = new FakePropertyForUpdateDto { }.Generate();
            await InsertAsync(fakePropertyOne);

            var property = await ExecuteDbContextAsync(db => db.Propertys.SingleOrDefaultAsync());
            var propertyId = property.PropertyId;

            // Act
            var command = new UpdateProperty.UpdatePropertyCommand(propertyId, updatedPropertyDto);
            await SendAsync(command);
            var updatedProperty = await ExecuteDbContextAsync(db => db.Propertys.Where(p => p.PropertyId == propertyId).SingleOrDefaultAsync());

            // Assert
            updatedProperty.Should().BeEquivalentTo(updatedPropertyDto, options =>
                options.ExcludingMissingMembers());
        }
    }
}