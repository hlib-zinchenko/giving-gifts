using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GivingGifts.SharedKernel.Infrastructure;

public static class ServiceScopeExtensions
{
    public static Task EnsureDbCreatedAndMigrated<T>(this IServiceScope serviceScope)
        where T : DbContext
    {
        var context = serviceScope.ServiceProvider.GetRequiredService<T>();
        return context.Database.MigrateAsync();
    }
}
