using System.Net;
using GivingGifts.SharedKernel.API.Resources.Mapping;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace GivingGifts.SharedKernel.API.Resources.FilterAttributes;

public class ValidateSortingAttribute<TDto, TResource> : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var dataShapingRequest = context
            .ActionArguments
            .Select(aa =>
                aa.Value)
            .OfType<ISortingRequest<TResource>>()
            .FirstOrDefault();

        if (dataShapingRequest == null)
        {
            throw new Exception($"Controller missing request parameter of {typeof(ISortingRequest<TResource>)}");
        }

        var dedicatedMapper = context
            .HttpContext
            .RequestServices
            .GetRequiredService<DedicatedResourceMapper<TDto, TResource>>();

        if (dedicatedMapper.ValidateRequestEntries(dataShapingRequest, out var invalidEntries))
        {
            return;
        }

        if (context.Controller is ControllerBase controller)
        {
            var validationMessage =
                $"Ordering for field(s) '{string.Join(", ", invalidEntries.Select(s => s.SortBy))}' " +
                $"is not possible";
            controller.ModelState.AddModelError(
                nameof(ISortingRequest.OrderBy),
                validationMessage);
            context.Result = controller.StatusCode(
                (int)HttpStatusCode.UnprocessableEntity,
                new ValidationProblemDetails(controller.ModelState));
        }
        else
        {
            throw new Exception(
                $"Attribute {nameof(ValidateSortingAttribute<TDto, TResource>)} should be applied only for {nameof(ControllerBase)} inheritors");
        }
    }
}