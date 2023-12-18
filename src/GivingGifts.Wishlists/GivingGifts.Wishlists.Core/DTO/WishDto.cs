namespace GivingGifts.Wishlists.Core.DTO;

public class WishDto
{
    public WishDto(Guid id, string name, string? url, string? notes)
    {
        Id = id;
        Name = name;
        Url = url;
        Notes = notes;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public WishDto()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    public Guid Id { get; init; }
    public string Name { get; init; }
    public string? Url { get; init; }
    public string? Notes { get; init; }
}