namespace NowProvisionAPI.SharedTestHelpers.Fakes.Property
{
    using AutoBogus;
    using NowProvisionAPI.Core.Dtos.Property;

    // or replace 'AutoFaker' with 'Faker' along with your own rules if you don't want all fields to be auto faked
    public class FakePropertyDto : AutoFaker<PropertyDto>
    {
        public FakePropertyDto()
        {
            // if you want default values on any of your properties (e.g. an int between a certain range or a date always in the past), you can add `RuleFor` lines describing those defaults
            //RuleFor(p => p.ExampleIntProperty, p => p.Random.Number(50, 100000));
            //RuleFor(p => p.ExampleDateProperty, p => p.Date.Past());
        }
    }
}