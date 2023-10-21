using GivingGifts.Users.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GivingGifts.Users.Infrastructure.Data.Configurations;

public class UserClaimConfiguration : IEntityTypeConfiguration<UserClaim>
{
    public void Configure(EntityTypeBuilder<UserClaim> builder)
    {
        builder
            .HasKey(s => s.Id);

        builder
            .ToTable("UserClaims");
    }
}