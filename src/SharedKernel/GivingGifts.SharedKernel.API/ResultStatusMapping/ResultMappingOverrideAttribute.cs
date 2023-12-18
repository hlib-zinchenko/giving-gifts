using System.Net;
using Ardalis.Result;

namespace GivingGifts.SharedKernel.API.ResultStatusMapping;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public sealed class ResultMappingOverrideAttribute(ResultStatus resultStatus, HttpStatusCode httpStatusCode)
    : Attribute, IResultMappingOverrideAttribute
{
    public HttpStatusCode HttpStatusCode { get; } = httpStatusCode;

    public ResultStatus ResultStatus { get; } = resultStatus;
}