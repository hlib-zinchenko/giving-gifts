using System.Net;
using GivingGifts.SharedKernel.Core.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GivingGifts.SharedKernel.API.Resources.FilterAttributes;

public class ValidateDataShapingAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var dataShapingRequest = context
            .ActionArguments
            .Select(aa =>
                aa.Value)
            .OfType<IDataShapingRequest>()
            .FirstOrDefault();

        if (dataShapingRequest == null)
        {
            throw new Exception($"Controller missing request parameter of {typeof(IDataShapingRequest)}");
        }

        var genericParameters = dataShapingRequest.GetType()
            .GetGeneticTypeParametersFromImplementedInterface(typeof(IDataShapingRequest<>))
            .ToArray();

        if (genericParameters.Length != 1)
        {
            return;
        }

        var resourceType = genericParameters[0];
        var resourceTypeProperties = resourceType
            .GetProperties()
            .Select(p => p.Name.ToLowerInvariant());

        var invalidFieldsValues = dataShapingRequest
            .GetDataShapingFields()
            .Where(f => !resourceTypeProperties.Contains(f))
            .ToArray();

        if (invalidFieldsValues.Length == 0)
        {
            return;
        }

        if (context.Controller is ControllerBase controller)
        {
            var validationMessage =
                $"Field(s) '{string.Join(", ", invalidFieldsValues)}' " +
                $"does not exist in the resource '{resourceType.Name}'";
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
                $"Attribute {nameof(ValidateDataShapingAttribute)} should be applied only for {nameof(ControllerBase)} inheritors");
        }
    }
}