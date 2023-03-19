using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeKju.Domain;

namespace ZeKju.Data.Mapping
{
    public class FlightMapping : EntityConfigurationMapper<Flight>
    {
        public static FlightMapping Instance = new();
        public override void Configure(EntityTypeBuilder<Flight> builder)
        {
            builder.ToTable(name: "Flights");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("flight_id");
            builder.Property(x => x.RouteId).HasColumnName("route_id");
            builder.Property(x => x.DepartureTime).HasColumnName("departure_time");
            builder.Property(x => x.ArrivalTime).HasColumnName("arrival_time");
            builder.Property(x => x.AirlineId).HasColumnName("airline_id");

            builder.HasOne(h => h.TheRoute).WithMany(w => w.Flights).HasForeignKey(h => h.RouteId);
        }
    }
}
