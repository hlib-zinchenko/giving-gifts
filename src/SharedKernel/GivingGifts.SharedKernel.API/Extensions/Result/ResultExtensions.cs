using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;

namespace GivingGifts.SharedKernel.API.Extensions.Result;

public static class ResultExtensions
{
    public static ActionResult<T> ToCreatedAtRouteActionResult<T>(
        this Result<T> result,
        ControllerBase controller,
        string? routeName,
        object? routeValues)
    {
        var resultStatusMap = new ResultStatusMap().AddDefaultMap();

        var resultStatusOptions = resultStatusMap[result.Status];
        var statusCode = (int)resultStatusOptions.GetStatusCode(controller.HttpContext.Request.Method);

        return result.Status switch
        {
            ResultStatus.Ok => controller.CreatedAtRoute(routeName, routeValues, result.GetValue()),
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            _ => resultStatusOptions.ResponseType == null
                ? (ActionResult)controller.StatusCode(statusCode)
                : controller.StatusCode(statusCode, resultStatusOptions.GetResponseObject(controller, result))
        };
    }
}