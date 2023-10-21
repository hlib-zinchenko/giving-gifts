namespace GivingGifts.SharedKernel.Core;

public interface IRepository<T>
    where T : IAggregationRoot
{
    Task SaveChangesAsync();
}