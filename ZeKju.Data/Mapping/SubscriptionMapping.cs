using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZeKju.Data.Mapping;

namespace ZeKju.Domain
{
    public class SubscriptionMapping : EntityConfigurationMapper<Subscription>
    {
        public static SubscriptionMapping Instance = new();
        public override void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.ToTable(name: "Subscriptions");
            builder.Ignore(x => x.Id);
            builder.HasKey(x => new { x.AgencyId, x.OriginCityId, x.DestinationCityId });
            builder.Property(x => x.AgencyId).HasColumnName("agency_id");
            builder.Property(x => x.OriginCityId).HasColumnName("origin_city_id");
            builder.Property(x => x.DestinationCityId).HasColumnName("destination_city_id");
        }
    }
}