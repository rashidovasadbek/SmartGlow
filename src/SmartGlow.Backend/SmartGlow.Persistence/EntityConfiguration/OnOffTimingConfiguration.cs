using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartGlow.Domain.Entities;

namespace SmartGlow.Persistence.EntityConfiguration;

public class OnOffTimingConfiguration : IEntityTypeConfiguration<OnOffTiming>
{
    public void Configure(EntityTypeBuilder<OnOffTiming> builder)
    {
        builder.Property(onOffTiming => onOffTiming.OnTime).IsRequired();
        builder.Property(onOffTiming => onOffTiming.OffTime).IsRequired();
        builder.Property(onOffTiming => onOffTiming.OffLights).IsRequired();
        builder.Property(onOffTiming => onOffTiming.OnLights).IsRequired();
        builder.Property(onOffTiming => onOffTiming.LitUnits).IsRequired();
        
        builder
            .HasOne(onOffTiming => onOffTiming.Street)
            .WithMany(street => street.OnOffTimings)
            .HasForeignKey(onOffTiming => onOffTiming.StreetId);
    }
}