namespace NowProvisionAPI.Infrastructure.Seeders
{

    using AutoBogus;
    using NowProvisionAPI.Core.Entities;
    using NowProvisionAPI.Infrastructure.Contexts;
    using System.Linq;

    public static class OfficeSeeder
    {
        public static void SeedSampleOfficeData(NowProvisionApiDbContext context)
        {
            if (!context.Offices.Any())
            {
                context.Offices.Add(new AutoFaker<Office>());
                context.Offices.Add(new AutoFaker<Office>());
                context.Offices.Add(new AutoFaker<Office>());

                context.SaveChanges();
            }
        }
    }
}