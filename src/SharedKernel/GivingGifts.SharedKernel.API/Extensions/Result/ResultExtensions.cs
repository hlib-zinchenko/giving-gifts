using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Ardalis.Result;
using GivingGifts.SharedKernel.Core;
using Microsoft.AspNetCore.Http;
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

    public static ActionResult<T[]> ToPagedActionResult<T>(
        this Result<PagedData<T>> result,
        ControllerBase controller,
        string? previousPageLink,
        string? nextPageLink)
    {
        var resultStatusMap = new ResultStatusMap().AddDefaultMap();

        var resultStatusOptions = resultStatusMap[result.Status];
        var statusCode = (int)resultStatusOptions.GetStatusCode(controller.HttpContext.Request.Method);

        if (result.Status != ResultStatus.Ok)
        {
            return resultStatusOptions.ResponseType == null
                ? controller.StatusCode(statusCode)
                : controller.StatusCode(
                    statusCode,
                    resultStatusOptions.GetResponseObject(controller, result));
        }

        var pagedInfo = new PagedInfo(
            result.Value.CurrentPage,
            result.Value.TotalCount,
            result.Value.PageSize,
            result.Value.TotalPages,
            previousPageLink,
            nextPageLink);

        controller.Response.Headers.Append(
            "X-PagedData", JsonSerializer.Serialize(pagedInfo));
        return controller.Ok(result.Value.Data);
    }

    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    private class PagedInfo(
        int currentPage,
        int totalCount,
        int pageSize,
        int totalPages,
        string? previousPageLink = null,
        string? nextPageLink = null)
    {
        public int CurrentPage { get; } = currentPage;
        public int TotalCount { get; } = totalCount;
        public int PageSize { get; } = pageSize;
        public int TotalPages { get; } = totalPages;
        public string? PreviousPageLink { get; } = previousPageLink;
        public string? NextPageLink { get; } = nextPageLink;
    }
}