namespace NowProvisionAPI.SharedTestHelpers.Fakes.Office
{
    using AutoBogus;
    using NowProvisionAPI.Core.Dtos.Office;

    // or replace 'AutoFaker' with 'Faker' along with your own rules if you don't want all fields to be auto faked
    public class FakeOfficeForCreationDto : AutoFaker<OfficeForCreationDto>
    {
        public FakeOfficeForCreationDto()
        {
            // if you want default values on any of your properties (e.g. an int between a certain range or a date always in the past), you can add `RuleFor` lines describing those defaults
            //RuleFor(o => o.ExampleIntProperty, o => o.Random.Number(50, 100000));
            //RuleFor(o => o.ExampleDateProperty, o => o.Date.Past());
        }
    }
}