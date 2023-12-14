using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Ardalis.Result;
using GivingGifts.SharedKernel.API.ResultStatusMapping;
using GivingGifts.SharedKernel.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using IResult = Ardalis.Result.IResult;

namespace GivingGifts.SharedKernel.API.Extensions.Result;

public static class ResultExtensions
{
    public static ActionResult ToActionResult(
        this IResult result,
        ControllerBase controller)
    {
        var resultStatusMap = controller.HttpContext.RequestServices.GetRequiredService<ResultStatusMap>();

        var attributes = controller
            .ControllerContext
            .ActionDescriptor.MethodInfo
            .GetCustomAttributes(false)
            .OfType<IResultMappingOverrideAttribute>();
        var overrides = attributes
            .ToDictionary(
                a => a.ResultStatus,
                a => a.HttpStatusCode);

        var resultStatusOptions = resultStatusMap[result.Status];
        return resultStatusOptions.Handle(result, controller, overrides);
    }

    public static ActionResult<T> ToActionResult<T>(
        this IResult result,
        ControllerBase controller)
    {
        var resultStatusMap = controller.HttpContext.RequestServices.GetRequiredService<ResultStatusMap>();

        var resultStatusOptions = resultStatusMap[result.Status];
        return resultStatusOptions.Handle(result, controller);
    }

    public static ActionResult<T> ToCreatedAtRouteActionResult<T>(
        this Result<T> result,
        ControllerBase controller,
        string? routeName,
        object? routeValues)
    {
        var resultStatusMap = controller.HttpContext.RequestServices.GetRequiredService<ResultStatusMap>();

        var resultStatusOptions = resultStatusMap[result.Status];
        return result.Status switch
        {
            ResultStatus.Ok => controller.CreatedAtRoute(routeName, routeValues, result.GetValue()),
            _ => resultStatusOptions.Handle(result, controller),
        };
    }

    public static ActionResult ToPagedActionResult<T>(
        this Result<PagedData<T>> result,
        ControllerBase controller,
        string? previousPageLink,
        string? nextPageLink)
    {
        var resultStatusMap = controller.HttpContext.RequestServices.GetRequiredService<ResultStatusMap>();

        var resultStatusOptions = resultStatusMap[result.Status];

        if (result.Status != ResultStatus.Ok)
        {
            return resultStatusOptions.Handle(result, controller);
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