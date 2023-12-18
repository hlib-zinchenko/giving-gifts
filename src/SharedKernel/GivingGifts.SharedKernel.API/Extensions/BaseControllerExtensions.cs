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
        var methods = OptionsMethod.Union(allowedMethods.Select(m => m.Method)).Distinct();
        controller.Response.Headers.Append("Allow", string.Join(", ", methods));
        return controller.Ok();
    }
}