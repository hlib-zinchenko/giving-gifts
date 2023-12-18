using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GivingGifts.SharedKernel.API.Resources.FilterAttributes;

public class ValidateDataShapingAttribute<TResource> : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var dataShapingRequest = context
            .ActionArguments
            .Select(aa =>
                aa.Value)
            .OfType<IDataShapingRequest<TResource>>()
            .FirstOrDefault();

        if (dataShapingRequest == null)
        {
            throw new Exception($"Controller missing request parameter of {typeof(IDataShapingRequest<TResource>)}");
        }

        if (dataShapingRequest.ValidateRequest(out var invalidFieldsValues))
        {
            return;
        }

        if (context.Controller is ControllerBase controller)
        {
            var validationMessage =
                $"Field(s) '{string.Join(", ", invalidFieldsValues)}' " +
                $"does not exist in the resource '{typeof(TResource).Name}'";
            controller.ModelState.AddModelError(
                nameof(IDataShapingRequest.Fields),
                validationMessage);
            context.Result = controller.StatusCode(
                (int)HttpStatusCode.UnprocessableEntity,
                new ValidationProblemDetails(controller.ModelState));
        }
        else
        {
            throw new Exception(
                $"Attribute {nameof(ValidateDataShapingAttribute<TResource>)} should be applied only for {nameof(ControllerBase)} inheritors");
        }
    }
}