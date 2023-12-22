namespace GivingGifts.SharedKernel.Core;

// ReSharper disable once UnusedTypeParameter
public interface IRepository<T>
    where T : IAggregationRoot
{
    Task SaveChangesAsync();
}