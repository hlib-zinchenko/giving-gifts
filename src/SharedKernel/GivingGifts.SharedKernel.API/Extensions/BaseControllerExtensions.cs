using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GivingGifts.SharedKernel.API.Extensions;

public static class BaseControllerExtensions
{
    private static readonly string[] OptionsMethod = { "OPTIONS" };

    public static ActionResult OptionsActionResult(
        this ControllerBase controller,
        params string[] allowedMethods)
    {
        var methods = OptionsMethod.Union(allowedMethods).Distinct();
        controller.Response.Headers.Append("Allow", string.Join(", ", methods));
        return controller.Ok();
    }
}