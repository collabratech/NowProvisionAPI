namespace Ordering.Infrastructure.Seeders
{

    using AutoBogus;
    using Ordering.Core.Entities;
    using Ordering.Infrastructure.Contexts;
    using System.Linq;

    public static class OfficeSeeder
    {
        public static void SeedSampleOfficeData(OrderingDbContext context)
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