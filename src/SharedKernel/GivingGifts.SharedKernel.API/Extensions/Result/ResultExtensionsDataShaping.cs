using System.Dynamic;
using System.Reflection;
using Ardalis.Result;
using GivingGifts.SharedKernel.API.Resources;
using GivingGifts.SharedKernel.Core;

namespace GivingGifts.SharedKernel.API.Extensions.Result;

public static class ResultExtensionsDataShaping
{
    internal static Result<ExpandoObject> Shape<T>(
        this Result<T> result,
        PropertyInfo[] propertiesToReturn)
    {
        return result.Map(resultData =>
        {
            var mappedResult = new ExpandoObject();
            for (var index = 0; index < propertiesToReturn.Length; index++)
            {
                var propertyInfo = propertiesToReturn[index];
                (mappedResult as IDictionary<string, object?>)[propertyInfo.Name]
                    = propertyInfo.GetValue(resultData);
            }

            return mappedResult;
        });
    }

    internal static Result<ExpandoObject[]> Shape<T>(
        this Result<IEnumerable<T>> result,
        PropertyInfo[] propertiesToReturn)
    {
        return result.Map(resultData =>
        {
            return resultData.Aggregate(new List<ExpandoObject>(), (accumulator, dataItem) =>
            {
                var mappedResult = new ExpandoObject();
                for (var index = 0; index < propertiesToReturn.Length; index++)
                {
                    var propertyInfo = propertiesToReturn[index];
                    (mappedResult as IDictionary<string, object?>)[propertyInfo.Name]
                        = propertyInfo.GetValue(dataItem);
                }

                accumulator.Add(mappedResult);
                return accumulator;
            }).ToArray();
        });
    }

    internal static Result<PagedData<ExpandoObject>> Shape<T>(
        this Result<PagedData<T>> result,
        PropertyInfo[] propertiesToReturn)
    {
        return result.Map(pagedData =>
        {
            return pagedData.Map(data => data.Select(dataItem =>
            {
                {
                    var mappedResult = new ExpandoObject();
                    for (var index = 0; index < propertiesToReturn.Length; index++)
                    {
                        var propertyInfo = propertiesToReturn[index];
                        (mappedResult as IDictionary<string, object?>)[propertyInfo.Name]
                            = propertyInfo.GetValue(dataItem);
                    }

                    return mappedResult;
                }
            }));
        });
    }
}