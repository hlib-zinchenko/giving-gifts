namespace GivingGifts.Wishlists.Core.DTO;

public class WishlistDto
{
    public WishlistDto(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public WishlistDto()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    public Guid Id { get; init; }
    public string Name { get; init; }
}