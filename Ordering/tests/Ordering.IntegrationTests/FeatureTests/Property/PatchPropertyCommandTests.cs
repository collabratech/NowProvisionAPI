namespace Ordering.IntegrationTests.FeatureTests.Property
{
    using Ordering.SharedTestHelpers.Fakes.Property;
    using Ordering.IntegrationTests.TestUtilities;
    using Ordering.Core.Dtos.Property;
    using Ordering.Core.Exceptions;
    using Ordering.WebApi.Features.Propertys;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.JsonPatch;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using static TestFixture;

    public class PatchPropertyCommandTests : TestBase
    {
        [Test]
        public async Task PatchPropertyCommand_Updates_Existing_Property_In_Db()
        {
            // Arrange
            var fakePropertyOne = new FakeProperty { }.Generate();
            await InsertAsync(fakePropertyOne);
            var property = await ExecuteDbContextAsync(db => db.Propertys.SingleOrDefaultAsync());
            var propertyId = property.PropertyId;

            var patchDoc = new JsonPatchDocument<PropertyForUpdateDto>();
            var newValue = "Easily Identified Value For Test";
            patchDoc.Replace(p => p.Slug, newValue);

            // Act
            var command = new PatchProperty.PatchPropertyCommand(propertyId, patchDoc);
            await SendAsync(command);
            var updatedProperty = await ExecuteDbContextAsync(db => db.Propertys.Where(p => p.PropertyId == propertyId).SingleOrDefaultAsync());

            // Assert
            updatedProperty.Slug.Should().Be(newValue);
        }
        
        [Test]
        public async Task PatchPropertyCommand_Throws_KeyNotFoundException_When_Bad_PK()
        {
            // Arrange
            var badId = Guid.NewGuid();
            var patchDoc = new JsonPatchDocument<PropertyForUpdateDto>();

            // Act
            var command = new PatchProperty.PatchPropertyCommand(badId, patchDoc);
            Func<Task> act = () => SendAsync(command);

            // Assert
            act.Should().Throw<KeyNotFoundException>();
        }

        [Test]
        public async Task PatchPropertyCommand_Throws_ApiException_When_Null_Patchdoc()
        {
            // Arrange
            var randomId = Guid.NewGuid();

            // Act
            var command = new PatchProperty.PatchPropertyCommand(randomId, null);
            Func<Task> act = () => SendAsync(command);

            // Assert
            act.Should().Throw<ApiException>();
        }
    }
}