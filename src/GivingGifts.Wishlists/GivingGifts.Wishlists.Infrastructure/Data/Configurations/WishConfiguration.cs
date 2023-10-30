using GivingGifts.SharedKernel.Core.Constants;
using GivingGifts.SharedKernel.Infrastructure;
using GivingGifts.Wishlists.Core.WishlistAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GivingGifts.Wishlists.Infrastructure.Data.Configurations;

public class WishConfiguration : IEntityTypeConfiguration<Wish>
{
    public void Configure(EntityTypeBuilder<Wish> builder)
    {
        builder
            .ConfigureEntityBase<Wish, Guid>("Wishes");

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