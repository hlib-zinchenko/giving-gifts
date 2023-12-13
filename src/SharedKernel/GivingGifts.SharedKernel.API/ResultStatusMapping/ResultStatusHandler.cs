using System.Net;
using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;

namespace GivingGifts.SharedKernel.API.ResultStatusMapping;

public class ResultStatusHandler
{
    private readonly ResultStatus _resultStatus;
    private HttpStatusCode DefaultStatusCode { get; }
    private Func<IResult, HttpStatusCode, ControllerBase, ActionResult>? _handler;
    private readonly Dictionary<string, HttpStatusCode> _specifiedStatusCodes = [];

    public ResultStatusHandler(
        ResultStatus resultStatus,
        HttpStatusCode defaultStatusCode)
    {
        _resultStatus = resultStatus;
        DefaultStatusCode = defaultStatusCode;
    }

    public ResultStatusHandler WithHandler(
        Func<IResult, HttpStatusCode, ControllerBase, ActionResult> func)
    {
        _handler = func;
        return this;
    }

    public ResultStatusHandler ForMethod(HttpMethod httpMethod, HttpStatusCode httpStatusCode)
    {
        _specifiedStatusCodes[httpMethod.Method] = httpStatusCode;
        return this;
    }

    private HttpStatusCode GetStatusCode(string httpMethod)
    {
        return _specifiedStatusCodes.GetValueOrDefault(httpMethod, DefaultStatusCode);
    }

    public ActionResult Handle(
        IResult result,
        ControllerBase controller,
        Dictionary<ResultStatus, HttpStatusCode>? statusCodesOverride = null)
    {
        var method = controller.Request.Method;
        var statusCode = statusCodesOverride != null
                         && statusCodesOverride.TryGetValue(_resultStatus, out var value)
            ? value
            : GetStatusCode(method);
        return _handler != null
            ? _handler(result, statusCode, controller)
            : new StatusCodeResult((int)statusCode);
    }
}