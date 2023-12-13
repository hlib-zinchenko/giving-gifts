using System.Net;
using Ardalis.Result;

namespace GivingGifts.SharedKernel.API.ResultStatusMapping;

public interface IResultMappingOverrideAttribute
{
    HttpStatusCode HttpStatusCode { get; }
    ResultStatus ResultStatus { get; }
}