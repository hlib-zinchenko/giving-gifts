namespace SharedKernel;

public interface IRepository<T>
    where T : IAggregationRoot
{
    Task SaveChangesAsync();
}