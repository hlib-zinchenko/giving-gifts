using GivingGifts.SharedKernel.Core.Constants;
using GivingGifts.SharedKernel.Infrastructure;
using GivingGifts.Wishlists.Core.WishlistAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GivingGifts.Wishlists.Infrastructure.Data.Configurations;

public class WishlistConfiguration : IEntityTypeConfiguration<Wishlist>
{
    public void Configure(EntityTypeBuilder<Wishlist> builder)
    {
        builder
            .ConfigureEntityBase<Wishlist, Guid>("Wishlists");

        builder
            .Property(w => w.Name)
            .HasMaxLength(PropertyLengthLimitation.Medium)
            .IsRequired();

        builder
            .Property(w => w.UserId)
            .IsRequired();
    }
}