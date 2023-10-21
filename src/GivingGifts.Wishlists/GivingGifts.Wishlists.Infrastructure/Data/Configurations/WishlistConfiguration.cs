using GivingGifts.SharedKernel.Core.Constants;
using GivingGifts.Wishlists.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GivingGifts.Wishlists.Infrastructure.Data.Configurations;

public class WishlistConfiguration : IEntityTypeConfiguration<Wishlist>
{
    public void Configure(EntityTypeBuilder<Wishlist> builder)
    {
        builder
            .ToTable("Wishlists");

        builder
            .HasKey(s => s.Id);

        builder
            .Property(w => w.Name)
            .HasMaxLength(PropertyLengthLimitation.Medium)
            .IsRequired();

        builder
            .Property(w => w.UserId)
            .IsRequired();
    }
}