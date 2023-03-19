using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeKju.Data.Mapping;
using ZeKju.Domain;

namespace ZeKju.Data.Contexts
{
    public class ZeKjuContext : DbContext
    {
        public ZeKjuContext(DbContextOptions options) : base(options) { }

        public DbSet<Route> Routes { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(RouteMapping.Instance);
            modelBuilder.ApplyConfiguration(FlightMapping.Instance);
            modelBuilder.ApplyConfiguration(SubscriptionMapping.Instance);
        }
    }
}
