namespace NowProvisionAPI.Infrastructure.Seeders
{

    using AutoBogus;
    using NowProvisionAPI.Core.Entities;
    using NowProvisionAPI.Infrastructure.Contexts;
    using System.Linq;

    public static class AgentSeeder
    {
        public static void SeedSampleAgentData(NowProvisionApiDbContext context)
        {
            if (!context.Agents.Any())
            {
                context.Agents.Add(new AutoFaker<Agent>());
                context.Agents.Add(new AutoFaker<Agent>());
                context.Agents.Add(new AutoFaker<Agent>());

                context.SaveChanges();
            }
        }
    }
}