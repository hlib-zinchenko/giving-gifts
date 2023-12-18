using System.ComponentModel;
using System.Linq.Expressions;
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

    public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, SortingParameter[] sortingParams)
    {
        if (sortingParams.Length == 0)
        {
            return source;
        }

        var type = typeof(T);
        var typeProperties = type
            .GetProperties()
            .Where(p => sortingParams.Any(sp => string.Equals(
                p.Name,
                sp.SortBy,
                StringComparison.OrdinalIgnoreCase)))
            .ToArray();

        var sorting = sortingParams.Select((sp) =>
                new
                {
                    sp.SortDirection,
                    Property = typeProperties.FirstOrDefault(p => string.Equals(
                        p.Name,
                        sp.SortBy,
                        StringComparison.OrdinalIgnoreCase))
                })
            .ToArray();
        if (sorting.Any(sp => sp.Property == null))
        {
            throw new Exception();
        }

        var queryExpr = source.Expression;

        for (var i = 0; i < sorting.Length; i++)
        {
            var sort = sorting[i];
            var methodName = i == 0
                ? sort.SortDirection == ListSortDirection.Ascending
                    ? "OrderBy"
                    : "OrderByDescending"
                : sort.SortDirection == ListSortDirection.Ascending
                    ? "ThenBy"
                    : "ThenByDescending";

            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, sort.Property!);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);
            var typeArguments = new[] { type, sort.Property!.PropertyType };
            queryExpr = Expression.Call(
                typeof(Queryable),
                methodName,
                typeArguments,
                queryExpr,
                Expression.Quote(orderByExp));
        }

        return source.Provider.CreateQuery<T>(queryExpr);
    }
}