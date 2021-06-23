namespace NowProvisionAPI.Infrastructure.Seeders
{

    using AutoBogus;
    using NowProvisionAPI.Core.Entities;
    using NowProvisionAPI.Infrastructure.Contexts;
    using System.Linq;

    public static class PropertySeeder
    {
        public static void SeedSamplePropertyData(NowProvisionApiDbContext context)
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