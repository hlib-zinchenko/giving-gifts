using System.Dynamic;
using System.Reflection;
using Ardalis.Result;
using GivingGifts.SharedKernel.API.Models;
using GivingGifts.SharedKernel.Core;

namespace GivingGifts.SharedKernel.API.Extensions.Result;

public static class ResultExtensionsDataShaping
{
    public static Result<ExpandoObject> Shape<T>(this Result<T> result, IDataShapingRequest request)
    {
        var propertiesToReturn = GetPropertiesToReturn<T>(request);
        return result.Map(resultData =>
        {
            var mappedResult = new ExpandoObject();
            foreach (var propertyInfo in propertiesToReturn)
            {
                (mappedResult as IDictionary<string, object?>)[propertyInfo.Name]
                    = propertyInfo.GetValue(resultData);
            }

            return mappedResult;
        });
    }

    public static Result<ExpandoObject[]> Shape<T>(this Result<T[]> result, IDataShapingRequest request)
    {
        var propertiesToReturn = GetPropertiesToReturn<T>(request);

        return result.Map(resultData =>
        {
            return resultData.Aggregate(new List<ExpandoObject>(), (accumulator, dataItem) =>
            {
                var mappedResult = new ExpandoObject();
                foreach (var propertyInfo in propertiesToReturn)
                {
                    (mappedResult as IDictionary<string, object?>)[propertyInfo.Name]
                        = propertyInfo.GetValue(dataItem);
                }

                accumulator.Add(mappedResult);
                return accumulator;
            }).ToArray();
        });
    }

    public static Result<PagedData<ExpandoObject>> Shape<T>(this Result<PagedData<T>> result,
        IDataShapingRequest request)
    {
        var propertiesToReturn = GetPropertiesToReturn<T>(request);
        return result.Map(pagedData =>
        {
            return pagedData.Map(dataItem =>
            {
                var mappedResult = new ExpandoObject();
                foreach (var propertyInfo in propertiesToReturn)
                {
                    (mappedResult as IDictionary<string, object?>)[propertyInfo.Name]
                        = propertyInfo.GetValue(dataItem);
                }

                return mappedResult;
            });
        });
    }

    private static IEnumerable<PropertyInfo> GetPropertiesToReturn<T>(IDataShapingRequest request)
    {
        var properties = typeof(T)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var requestedFields = request.GetFields().ToArray();
        var propertiesToReturn = requestedFields.Length == 0
            ? properties
            : properties.Where(p =>
                requestedFields.Any(rf => rf
                    .Equals(p.Name, StringComparison.InvariantCultureIgnoreCase)));
        return propertiesToReturn;
    }
}