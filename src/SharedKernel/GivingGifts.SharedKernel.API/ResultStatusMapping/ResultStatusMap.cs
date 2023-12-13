using System.Net;
using System.Text;
using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;

namespace GivingGifts.SharedKernel.API.ResultStatusMapping;

public class ResultStatusMap
{
    private readonly Dictionary<ResultStatus, ResultStatusHandler> _map = [];

    private ResultStatusMap()
    {
    }

    public static ResultStatusMap CreateDefault()
    {
        var statusMap = new ResultStatusMap()
            .For(ResultStatus.Ok, HttpStatusCode.OK,
                configureHandle => configureHandle
                    .WithHandler(SuccessHandler)
                    .ForMethod(HttpMethod.Post, HttpStatusCode.Created)
                    .ForMethod(HttpMethod.Put, HttpStatusCode.NoContent))
            .For(ResultStatus.Forbidden, HttpStatusCode.Forbidden)
            .For(ResultStatus.Unauthorized, HttpStatusCode.Unauthorized)
            .For(ResultStatus.Error, HttpStatusCode.UnprocessableEntity, resultStatusOptions => resultStatusOptions
                .WithHandler(UnprocessableEntityHandler))
            .For(ResultStatus.Invalid, HttpStatusCode.UnprocessableEntity, resultStatusOptions => resultStatusOptions
                .WithHandler(UnprocessableEntityHandler))
            .For(ResultStatus.NotFound, HttpStatusCode.NotFound, resultStatusOptions => resultStatusOptions
                .WithHandler(NotFoundHandler))
            .For(ResultStatus.Conflict, HttpStatusCode.Conflict, resultStatusOptions => resultStatusOptions
                .WithHandler(ConflictHandler))
            .For(ResultStatus.CriticalError, HttpStatusCode.InternalServerError, resultStatusOptions =>
                resultStatusOptions
                    .WithHandler(CriticalErrorHandler))
            .For(ResultStatus.Unavailable, HttpStatusCode.ServiceUnavailable, resultStatusOptions =>
                resultStatusOptions
                    .WithHandler(UnavailableHandler));

        return statusMap;
    }

    public ResultStatusHandler this[ResultStatus resultStatus] => _map[resultStatus];

    public ResultStatusMap For(
        ResultStatus status,
        HttpStatusCode httpStatusCode,
        Action<ResultStatusHandler>? configureHandle = null)
    {
        var resultHandler = new ResultStatusHandler(status, httpStatusCode);
        configureHandle?.Invoke(resultHandler);
        _map[status] = resultHandler;
        return this;
    }

    private static ActionResult SuccessHandler(IResult result, HttpStatusCode statusCode, ControllerBase controller)
    {
        return result is Result
            ? controller.StatusCode((int)statusCode)
            : controller.StatusCode((int)statusCode, result.GetValue());
    }

    private static ObjectResult ErrorHandlerResultFormatter(IResult result, HttpStatusCode statusCode, ControllerBase controller, string title)
    {
        var details = new StringBuilder("Next error(s) occurred:");
        foreach (var error in result.Errors) details.Append("* ").Append(error).AppendLine();
        return controller.StatusCode((int)statusCode, new ProblemDetails
        {
            Title = title,
            Detail = result.Errors.Any() ? details.ToString() : null
        });
    }

    private static ActionResult UnprocessableEntityHandler(IResult result, HttpStatusCode statusCode, ControllerBase controller)
    {
        return ErrorHandlerResultFormatter(result, statusCode, controller, "Something went wrong.");
    }

    private static ActionResult NotFoundHandler(IResult result, HttpStatusCode statusCode, ControllerBase controller)
    {
        return ErrorHandlerResultFormatter(result, statusCode, controller, "Resource not found.");
    }

    private static ActionResult ConflictHandler(IResult result, HttpStatusCode statusCode, ControllerBase controller)
    {
        return ErrorHandlerResultFormatter(result, statusCode, controller, "There was a conflict.");
    }

    private static ActionResult CriticalErrorHandler(IResult result, HttpStatusCode statusCode, ControllerBase controller)
    {
        return ErrorHandlerResultFormatter(result, statusCode, controller, "Something went wrong.");
    }

    private static ActionResult UnavailableHandler(IResult result, HttpStatusCode statusCode, ControllerBase controller)
    {
        return ErrorHandlerResultFormatter(result, statusCode, controller, "Service is unavailable.");
    }
}