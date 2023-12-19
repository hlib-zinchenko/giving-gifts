using System.Net;
using GivingGifts.SharedKernel.API.Extensions;
using GivingGifts.SharedKernel.API.Resources.Mapping;
using GivingGifts.SharedKernel.API.Resources.RequestValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace GivingGifts.SharedKernel.API.Resources.FilterAttributes;

public class ValidateResourceRequestAttribute<TResource> : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var request = context
            .ActionArguments
            .Select(aa =>
                aa.Value)
            .OfType<ResourceRequestBase<TResource>>()
            .FirstOrDefault();

        if (request == null)
        {
            throw new Exception($"Controller missing request parameter of {typeof(ResourcesRequestBase<TResource>)}");
        }

        var validator = context
            .HttpContext
            .RequestServices
            .GetRequiredService<IResourceRequestValidator>();

        var validationResult = validator.Validate(request);
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

            controller.AddShapableFieldsHeader(
                validationResult.DataShapingRequestValidationResult.ValidToRequestProperties);
            context.Result = controller.StatusCode(
                (int)HttpStatusCode.UnprocessableEntity,
                new ValidationProblemDetails(controller.ModelState));
        }
        else
        {
            throw new Exception(
                $"Attribute {nameof(ValidateResourceRequestAttribute<TResource>)} should be applied only for {nameof(ControllerBase)} inheritors");
        }
    }
}