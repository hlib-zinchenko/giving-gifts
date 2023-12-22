using System.Reflection;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;
using MediatR;
using ValidationException = FluentValidation.ValidationException;

namespace GivingGifts.SharedKernel.Core.Behaviours;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);
        var validationResults = await Task.WhenAll(
            _validators.Select(v => v.ValidateAsync(context, cancellationToken)));
        var invalidValidationResults = validationResults
            .Where(r => !r.IsValid).ToArray();
        if (invalidValidationResults.Length == 0)
        {
            return await next();
        }

        var responseType = typeof(TResponse);
        if (responseType.IsGenericType && responseType.GetGenericTypeDefinition() != typeof(Result<>) &&
            responseType != typeof(Result))
        {
            throw new ValidationException(invalidValidationResults
                .SelectMany(vr => vr.Errors));
        }

        var errors = invalidValidationResults
            .SelectMany(r => r.AsErrors()).ToArray();
        var errorsType = errors.GetType();

        // Find the "Invalid" method on the Result type
        var invalidMethod = responseType.GetMethods(
            BindingFlags.Public | BindingFlags.Static)
            .Where(m => m.Name == "Invalid"
                        && m.GetParameters().Length == 1
                        && m.GetParameters()[0].ParameterType == errorsType)
            .ToArray();

        if (invalidMethod == null)
        {
            throw new InvalidOperationException("Invalid method not found on the Result type.");
        }

        var errorResult = invalidMethod[0].Invoke(null, new object[] { errors });
        return (TResponse)errorResult!;
    }
}
