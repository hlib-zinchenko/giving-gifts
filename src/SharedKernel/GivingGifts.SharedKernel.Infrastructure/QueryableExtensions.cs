using GivingGifts.SharedKernel.Core;
using Microsoft.EntityFrameworkCore;

namespace GivingGifts.SharedKernel.Infrastructure;

public static class QueryableExtensions
{
    public static async Task<PagedData<T>> ToPagedDataAsync<T>(this IQueryable<T> query, int page, int pageSize)
    {
        var totalCount = await query.CountAsync();
        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedData<T>(items, page, pageSize, totalCount);
    }
}