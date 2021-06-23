namespace Ordering.Infrastructure.Seeders
{

    using AutoBogus;
    using Ordering.Core.Entities;
    using Ordering.Infrastructure.Contexts;
    using System.Linq;

    public static class PropertySeeder
    {
        public static void SeedSamplePropertyData(OrderingDbContext context)
        {
            if (!context.Propertys.Any())
            {
                context.Propertys.Add(new AutoFaker<Property>());
                context.Propertys.Add(new AutoFaker<Property>());
                context.Propertys.Add(new AutoFaker<Property>());

                context.SaveChanges();
            }
        }
    }
}