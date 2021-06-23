namespace Ordering.Infrastructure.Seeders
{

    using AutoBogus;
    using Ordering.Core.Entities;
    using Ordering.Infrastructure.Contexts;
    using System.Linq;

    public static class OrderSeeder
    {
        public static void SeedSampleOrderData(OrderingDbContext context)
        {
            if (!context.Orders.Any())
            {
                context.Orders.Add(new AutoFaker<Order>());
                context.Orders.Add(new AutoFaker<Order>());
                context.Orders.Add(new AutoFaker<Order>());

                context.SaveChanges();
            }
        }
    }
}