using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GivingGifts.SharedKernel.Data;

public static class ServiceScopeExtensions
{
    public static async Task EnsureDbCreatedAndMigrated<T>(this IServiceScope serviceScope)
        where T : DbContext
    {
        var context = serviceScope.ServiceProvider.GetRequiredService<T>();
        await context.Database.MigrateAsync();
    }
}
