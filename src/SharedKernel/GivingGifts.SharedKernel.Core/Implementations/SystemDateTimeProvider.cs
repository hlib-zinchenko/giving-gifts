namespace GivingGifts.SharedKernel.Core.Implementations;

internal class SystemDateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}