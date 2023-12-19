using Ardalis.Result;
using GivingGifts.SharedKernel.API.Models;
using GivingGifts.SharedKernel.API.Resources;
using GivingGifts.SharedKernel.API.Resources.RequestValidation;
using GivingGifts.SharedKernel.API.ResultStatusMapping;
using GivingGifts.SharedKernel.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using IResult = Ardalis.Result.IResult;

namespace GivingGifts.SharedKernel.API.Extensions.Result;

public static class ResultExtensions
{
    public static ActionResult ToActionResult<TResource>(
        this Result<TResource> result,
        ResourceRequestBase<TResource> request,
        ControllerBase controller)
    {
        var shapingRequestValidator = controller.HttpContext.RequestServices
            .GetRequiredService<IDataShapingRequestValidator>();
        var shapableFields = shapingRequestValidator.GetValidToRequestFields<TResource>();
        if (shapingRequestValidator.ShouldApplyShaping(request, out var shapeReadyFields))
        {
            return result
                .Shape(shapeReadyFields)
                .ToActionResult(controller, shapableFields);
        }

        return result.ToActionResult(controller, shapableFields);
    }

    public static ActionResult ToActionResult<TResource>(
        this Result<IEnumerable<TResource>> result,
        ResourceRequestBase<TResource> request,
        ControllerBase controller)
    {
        var shapingRequestValidator = controller.HttpContext.RequestServices
            .GetRequiredService<IDataShapingRequestValidator>();
        var shapableFields = shapingRequestValidator.GetValidToRequestFields<TResource>();
        if (shapingRequestValidator.ShouldApplyShaping(request, out var shapeReadyFields))
        {
            return result
                .Shape(shapeReadyFields)
                .ToActionResult(controller, shapableFields);
        }

        return result.ToActionResult(controller, shapableFields);
    }

    public static ActionResult ToActionResult(
        this IResult result,
        ControllerBase controller,
        string[]? shapableFields = null)
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

        controller.AddShapableFieldsHeader(shapableFields);

        var resultStatusOptions = resultStatusMap[result.Status];
        return resultStatusOptions.Handle(result, controller, overrides);
    }

    public static ActionResult ToCreatedAtRouteActionResult<T>(
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

    public static ActionResult ToPagedActionResult<TDto, TResource>(
        this Result<PagedData<TResource>> result,
        ControllerBase controller,
        ResourcesRequestBase<TResource> request,
        string? previousPageLink,
        string? nextPageLink)
    {
        var sortingRequestValidator = controller.HttpContext.RequestServices
            .GetRequiredService<ISortingRequestValidator>();
        var sortableFields = sortingRequestValidator.GetValidToRequestFields<TResource, TDto>(request);
        
        var shapingRequestValidator = controller.HttpContext.RequestServices
            .GetRequiredService<IDataShapingRequestValidator>();
        var shapableFields = shapingRequestValidator.GetValidToRequestFields<TResource>();
        if (shapingRequestValidator.ShouldApplyShaping(request, out var shapeReadyFields))
        {
            return result
                .Shape(shapeReadyFields)
                .ToPagedActionResult(
                    controller,
                    previousPageLink,
                    nextPageLink,
                    shapableFields,
                    sortableFields);
        }

        return result
            .ToPagedActionResult(
                controller,
                previousPageLink,
                nextPageLink,
                shapableFields,
                sortableFields);
    }

    private static ActionResult ToPagedActionResult<T>(
        this Result<PagedData<T>> result,
        ControllerBase controller,
        string? previousPageLink,
        string? nextPageLink,
        string[] shapableFields,
        string[] sortableFields)
    {
        var resultStatusMap = controller.HttpContext.RequestServices.GetRequiredService<ResultStatusMap>();

        var resultStatusOptions = resultStatusMap[result.Status];

        if (result.Status != ResultStatus.Ok)
        {
            return resultStatusOptions.Handle(result, controller);
        }

        var pagingMetadata = new PagingMetadata(
            result.Value.CurrentPage,
            result.Value.TotalCount,
            result.Value.PageSize,
            result.Value.TotalPages,
            previousPageLink,
            nextPageLink);

        return controller
            .AddShapableFieldsHeader(shapableFields)
            .AddSortableFieldsHeader(sortableFields)
            .AddPagedDataHeader(pagingMetadata)
            .Ok(result.Value.Data);
    }
}