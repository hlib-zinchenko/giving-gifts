namespace GivingGifts.SharedKernel.API.Resources;

public interface IPaginatingRequest
{
    int Page { get; set; }

    int PageSize { get; set; }
}