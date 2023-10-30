using GivingGifts.SharedKernel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GivingGifts.SharedKernel.Infrastructure;

public static class EntityTypeBuildExtensions
{
    public static EntityTypeBuilder<TEntity> ConfigureEntityBase<TEntity, TKey>(this EntityTypeBuilder<TEntity> builder,
        string toTable)
        where TEntity : class, IEntity<TKey>
    {
        builder.ToTable(toTable);

        builder
            .HasKey(s => s.Id);

        builder
            .Property(w => w.Id)
            .ValueGeneratedNever();

        return builder;
    }
}