namespace NowProvisionAPI.Infrastructure.Contexts
{
    using NowProvisionAPI.Core.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using System.Threading;
    using System.Threading.Tasks;

    public class NowProvisionApiDbContext : DbContext
    {
        public NowProvisionApiDbContext(
            DbContextOptions<NowProvisionApiDbContext> options) : base(options)
        {
        }

        #region DbSet Region - Do Not Delete

        public DbSet<NowProv> NowProvs { get; set; }
        public DbSet<Property> Propertys { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Office> Offices { get; set; }
        #endregion DbSet Region - Do Not Delete



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NowProv>().Property(p => p.Id).ValueGeneratedNever();
            modelBuilder.Entity<Property>().Property(p => p.PropertyId).ValueGeneratedNever();
            modelBuilder.Entity<Agent>().Property(p => p.AgentId).ValueGeneratedNever();
            modelBuilder.Entity<Office>().Property(p => p.OfficeId).ValueGeneratedNever();
        }
    }
}