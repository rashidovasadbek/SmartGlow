using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartGlow.Domain.Entities;
using SmartGlow.Domain.Enums;

namespace SmartGlow.Persistence.EntityConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(client => client.FirstName).HasMaxLength(64).IsRequired();
        builder.Property(client => client.LastName).HasMaxLength(64).IsRequired();
        builder.Property(client => client.UserName).HasMaxLength(128).IsRequired();
        builder.Property(client => client.PasswordHash).HasMaxLength(128).IsRequired();
        
        
        builder
            .Property(client => client.Role)
            .HasConversion<string>()
            .HasDefaultValue(RoleType.User)
            .HasMaxLength(64)
            .IsRequired();
    }
}