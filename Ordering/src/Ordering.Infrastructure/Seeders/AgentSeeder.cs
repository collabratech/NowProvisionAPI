namespace Ordering.Infrastructure.Seeders
{

    using AutoBogus;
    using Ordering.Core.Entities;
    using Ordering.Infrastructure.Contexts;
    using System.Linq;

    public static class AgentSeeder
    {
        public static void SeedSampleAgentData(OrderingDbContext context)
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