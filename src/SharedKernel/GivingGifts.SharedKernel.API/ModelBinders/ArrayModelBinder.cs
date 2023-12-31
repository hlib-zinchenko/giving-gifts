using System.ComponentModel;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GivingGifts.SharedKernel.API.ModelBinders;

public class ArrayModelBinder : IModelBinder
{
    private const string Separator = ",";

    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        // Our binder works only on enumerable types
        if (!bindingContext.ModelMetadata.IsEnumerableType)
        {
            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;
        }

        // Get the inputted value through the value provider
        var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).ToString();

        // If that value is null or whitespace, we return null
        if (string.IsNullOrWhiteSpace(value))
        {
            bindingContext.Result = ModelBindingResult.Success(null);
            return Task.CompletedTask;
        }

        // The value isn't null or whitespace,
        // and the type of the model is enumerable.
        // Get the enumerable's type, and a converter
        var bindingModelType = bindingContext.ModelType.GetTypeInfo();
        var elementType = bindingModelType.IsArray
            ? bindingModelType.GetElementType()
            : bindingModelType.GenericTypeArguments[0];
        if (elementType == null)
        {
            throw new InvalidOperationException();
        }
        
        var converter = TypeDescriptor.GetConverter(elementType);

        // Convert each item in the value list to the enumerable type
        var values = value.Split(Separator,
                StringSplitOptions.RemoveEmptyEntries)
            .Select(x => converter.ConvertFromString(x.Trim()))
            .ToArray();

        // Create an array of that type, and set it as the Model value
        var typedValues = Array.CreateInstance(elementType, values.Length);
        values.CopyTo(typedValues, 0);
        bindingContext.Model = typedValues;

        // return a successful result, passing in the Model
        bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);
        return Task.CompletedTask;
    }
}