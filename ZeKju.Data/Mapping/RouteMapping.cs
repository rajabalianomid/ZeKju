using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZeKju.Data.Mapping;

namespace ZeKju.Domain
{
    public class RouteMapping : EntityConfigurationMapper<Route>
    {
        public static RouteMapping Instance = new();
        public override void Configure(EntityTypeBuilder<Route> builder)
        {
            builder.ToTable(name: "Routes");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("route_id");
            builder.Property(x => x.OriginCityId).HasColumnName("origin_city_id");
            builder.Property(x => x.DestinationCityId).HasColumnName("destination_city_id");
            builder.Property(x => x.DepartureDate).HasColumnName("departure_date");

            builder.HasMany(h => h.Flights).WithOne(w => w.TheRoute).HasForeignKey(h => h.RouteId);
        }
    }
}