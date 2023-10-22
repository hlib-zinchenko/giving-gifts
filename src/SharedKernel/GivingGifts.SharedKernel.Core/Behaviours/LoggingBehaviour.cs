using System.Diagnostics;
using System.Text.Json;
using GivingGifts.SharedKernel.Core.JsonConverters;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GivingGifts.SharedKernel.Core.Behaviours;

public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly Stopwatch _timer;
    private readonly ILogger<TRequest> _logger;

    public LoggingBehaviour(ILogger<TRequest> logger)
    {
        _timer = new Stopwatch();
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestType = typeof(TRequest);
        var requestName = requestType.Name;
        _logger.LogInformation("Start processing command/query {requestType}", requestName);
        _timer.Start();

        var response = await next().ConfigureAwait(false);

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;
        _timer.Stop();

        if (elapsedMilliseconds <= 1000)
        {
            _logger.LogInformation(
                "Finished processing command/query {requestType} in {elapsedMilliseconds} milliseconds",
                requestName,
                elapsedMilliseconds);
        }
        else
        {
            var options = new JsonSerializerOptions();
            options.Converters.Add(new SensitiveDataConverter());
            var requestData = JsonSerializer.Serialize(request as object, options);
            _logger.LogWarning(
                "Performance issue: Finished processing command/query {requestType} in {elapsedMilliseconds} milliseconds with {requestData}",
                requestName,
                elapsedMilliseconds,
                requestData);
        }

        return response;
    }
}