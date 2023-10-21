using GivingGifts.SharedKernel.Core.Constants;
using GivingGifts.Users.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GivingGifts.Users.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasKey(s => s.Id);

        builder
            .ToTable("Users");

        builder
            .Property(p => p.FirstName)
            .HasMaxLength(PropertyLengthLimitation.Medium)
            .IsRequired();

        builder
            .Property(p => p.LastName)
            .HasMaxLength(PropertyLengthLimitation.Medium)
            .IsRequired();

        builder
            .Property(p => p.Email)
            .HasMaxLength(PropertyLengthLimitation.Medium)
            .IsRequired();
    }
}