namespace GivingGifts.SharedKernel.API.Models;

public interface IPaginateRequest
{
    public int Page { get; set; }

    public int PageSize { get; set; }
}