namespace NowProvisionAPI.Infrastructure.Seeders
{

    using AutoBogus;
    using NowProvisionAPI.Core.Entities;
    using NowProvisionAPI.Infrastructure.Contexts;
    using System.Linq;

    public static class NowProvSeeder
    {
        public static void SeedSampleNowProvData(NowProvisionApiDbContext context)
        {
            if (!context.NowProvs.Any())
            {
                context.NowProvs.Add(new AutoFaker<NowProv>());
                context.NowProvs.Add(new AutoFaker<NowProv>());
                context.NowProvs.Add(new AutoFaker<NowProv>());

                context.SaveChanges();
            }
        }
    }
}