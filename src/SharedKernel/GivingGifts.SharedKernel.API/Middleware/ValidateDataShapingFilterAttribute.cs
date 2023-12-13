using GivingGifts.SharedKernel.API.Models;
using GivingGifts.SharedKernel.Core.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GivingGifts.SharedKernel.API.Middleware;

public class ValidateDataShapingFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var dataShapingRequest = context
            .ActionArguments
            .Select(aa => aa.Value)
            .FirstOrDefault(a =>
                a is IDataShapingRequest);

        if (dataShapingRequest == null)
        {
            base.OnActionExecuting(context);
            return;
        }

        var genericParameters = dataShapingRequest.GetType()
            .GetGeneticTypeParametersFromImplementedInterface(typeof(IDataShapingRequest<>))
            .ToArray();

        if (genericParameters.Length != 1)
        {
            base.OnActionExecuting(context);
            return;
        }

        var resourceType = genericParameters[0];
        var resourceTypeProperties = resourceType
            .GetProperties()
            .Select(p => p.Name.ToLowerInvariant());

        var fields = (dataShapingRequest as IDataShapingRequest)!.GetFields();
        if (fields.Any(f => !resourceTypeProperties.Contains(f)))
        {
            context.Result = new BadRequestObjectResult("Invalid field(s) specified in the request.");
            return;
        }
        
        base.OnActionExecuting(context);
    }
}