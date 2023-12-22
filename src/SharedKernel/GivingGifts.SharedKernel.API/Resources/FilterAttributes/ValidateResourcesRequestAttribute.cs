using System.Net;
using GivingGifts.SharedKernel.API.Extensions;
using GivingGifts.SharedKernel.API.Resources.RequestValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace GivingGifts.SharedKernel.API.Resources.FilterAttributes;

public class ValidateResourcesRequestAttribute<TDto, TResource> : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var request = context
            .ActionArguments
            .Select(aa =>
                aa.Value)
            .OfType<ResourcesRequestBase<TResource>>()
            .FirstOrDefault();

        if (request == null)
        {
            throw new Exception($"Controller missing request parameter of {typeof(ResourcesRequestBase<TResource>)}");
        }

        var validator = context
            .HttpContext
            .RequestServices
            .GetRequiredService<IResourcesRequestValidator>();

        var validationResult = validator.Validate<TResource, TDto>(request);
        if (validationResult.IsValid)
        {
            return;
        }

        if (context.Controller is ControllerBase controller)
        {
            var validationErrors = validationResult.GetValidationErrors();
            foreach (var validationError in validationErrors)
            {
                controller.ModelState.AddModelError(
                    validationError.Key,
                    validationError.Value);
            }

            controller
                .AddShapableFieldsHeader(
                    validationResult.DataShapingRequestValidationResult.ValidToRequestProperties)
                .AddSortableFieldsHeader(
                    validationResult.SortingRequestValidationResult.ValidToRequestProperties);
            context.Result = controller.StatusCode(
                (int)HttpStatusCode.UnprocessableEntity,
                new ValidationProblemDetails(controller.ModelState));
        }
        else
        {
            throw new Exception(
                $"Attribute {nameof(ValidateResourcesRequestAttribute<TDto, TResource>)} should be applied only for {nameof(ControllerBase)} inheritors");
        }
    }
}