using System.Text.Json;
using GivingGifts.SharedKernel.API.Constants;
using GivingGifts.SharedKernel.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GivingGifts.SharedKernel.API.Extensions;

public static class BaseControllerExtensions
{
    private static readonly string[] OptionsMethod = { HttpMethod.Options.Method };

    public static ActionResult OptionsActionResult(
        this ControllerBase controller,
        params HttpMethod[] allowedMethods)
    {
        controller.AddAllowHeader(allowedMethods);
        return controller.Ok();
    }
    
    public static ControllerBase AddShapableFieldsHeader(this ControllerBase controller, string[]? shapableFields)
    {
        if (shapableFields != null)
        {
            controller.Response.Headers.Append(HttpHeaders.ShapableFields, string.Join(", ", shapableFields));
        }

        return controller;
    }
    
    public static ControllerBase AddPagedDataHeader(this ControllerBase controller, PagingMetadata pagingMetadata)
    {
        controller.Response.Headers.Append(
            HttpHeaders.PagedData, JsonSerializer.Serialize(pagingMetadata));

        return controller;
    }
    
    public static ControllerBase AddSortableFieldsHeader(this ControllerBase controller, string[] sortableFields)
    {
        controller.Response.Headers.Append(
            HttpHeaders.SortableFields, string.Join(", ", sortableFields));

        return controller;
    }
    
    public static ControllerBase AddAllowHeader(this ControllerBase controller, HttpMethod[] allowedMethods)
    {
        var methods = OptionsMethod.Union(allowedMethods.Select(m => m.Method)).Distinct();
        controller.Response.Headers.Append(HttpHeaders.Allow, string.Join(", ", methods));

        return controller;
    }
}