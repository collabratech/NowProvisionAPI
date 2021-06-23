namespace NowProvisionAPI.SharedTestHelpers.Fakes.NowProv
{
    using AutoBogus;
    using NowProvisionAPI.Core.Dtos.NowProv;

    // or replace 'AutoFaker' with 'Faker' along with your own rules if you don't want all fields to be auto faked
    public class FakeNowProvDto : AutoFaker<NowProvDto>
    {
        public FakeNowProvDto()
        {
            // if you want default values on any of your properties (e.g. an int between a certain range or a date always in the past), you can add `RuleFor` lines describing those defaults
            //RuleFor(n => n.ExampleIntProperty, n => n.Random.Number(50, 100000));
            //RuleFor(n => n.ExampleDateProperty, n => n.Date.Past());
        }
    }
}