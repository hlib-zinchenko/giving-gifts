using GivingGifts.SharedKernel.Core.Constants;
using GivingGifts.Wishlists.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GivingGifts.Wishlists.Infrastructure.Data.Configurations;

public class WishConfiguration : IEntityTypeConfiguration<Wish>
{
    public void Configure(EntityTypeBuilder<Wish> builder)
    {
        builder
            .ToTable("Wish");

        builder
            .HasKey(s => s.Id);

        builder
            .Property(w => w.Name)
            .HasMaxLength(PropertyLengthLimitation.Medium)
            .IsRequired();

        builder
            .Property(w => w.Url)
            .HasMaxLength(PropertyLengthLimitation.Giant)
            .IsRequired(false);
    }
}