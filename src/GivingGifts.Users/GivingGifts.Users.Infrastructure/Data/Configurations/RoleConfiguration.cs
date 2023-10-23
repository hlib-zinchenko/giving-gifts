using GivingGifts.SharedKernel.Core.Constants;
using GivingGifts.Users.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GivingGifts.Users.Infrastructure.Data.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder
            .HasKey(s => s.Id);

        builder
            .ToTable("Roles");

        builder
            .HasData(new List<Role>
            {
                new(new Guid("11A5BFD0-644D-48C8-9282-1416804FC165"), RoleNames.User),
                new(new Guid("944E347E-44F9-4F6A-A73E-652C472BE11A"), RoleNames.Admin),
            });
    }
}