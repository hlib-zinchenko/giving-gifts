using System.Net;
using Ardalis.Result;

namespace GivingGifts.SharedKernel.API.ResultStatusMapping;

[AttributeUsage(AttributeTargets.Method)]
public sealed class OkResultMappingOverrideAttribute(HttpStatusCode httpStatusCode)
    : Attribute, IResultMappingOverrideAttribute
{
    public HttpStatusCode HttpStatusCode { get; } = httpStatusCode;
    public ResultStatus ResultStatus => ResultStatus.Ok;
}