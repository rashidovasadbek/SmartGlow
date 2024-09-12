using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartGlow.Domain.Entities;

namespace SmartGlow.Persistence.EntityConfiguration;

public class StreetConfiguration : IEntityTypeConfiguration<Street>
{
    public void Configure(EntityTypeBuilder<Street> builder)
    {
        builder.Property(street => street.StreetName).IsRequired().HasMaxLength(256);
        builder.Property(street => street.Latitude).IsRequired();
        builder.Property(street => street.Longitude).IsRequired();
        
        builder
            .HasOne(street => street.User)
            .WithMany(user => user.Streets)
            .HasForeignKey(street => street.UserId);
        
    }
}