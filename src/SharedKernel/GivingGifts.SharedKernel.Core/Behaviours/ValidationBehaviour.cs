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
        if (!invalidValidationResults.Any())
        {
            return await next();
        }

        var errors = invalidValidationResults
            .SelectMany(r => r.AsErrors()).ToList();
        var responseType = typeof(TResponse);
        if ((responseType.IsGenericType && responseType.GetGenericTypeDefinition() != typeof(Result<>)) &&
            responseType != typeof(Result))
        {
            throw new ValidationException(invalidValidationResults
                .SelectMany(vr => vr.Errors));
        }

        var errorResult = responseType
            .GetMethod("Invalid", BindingFlags.Public | BindingFlags.Static)!
            .Invoke(null, new object[] { errors });
        return (TResponse)errorResult!;
    }
}